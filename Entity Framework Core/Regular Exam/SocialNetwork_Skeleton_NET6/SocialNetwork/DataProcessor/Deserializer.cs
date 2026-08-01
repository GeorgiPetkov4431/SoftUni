using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SocialNetwork.Data;
using SocialNetwork.Data.Models;
using SocialNetwork.Data.Models.Enums;
using SocialNetwork.DataProcessor.ImportDTOs;
using SocialNetwork.Utilities;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace SocialNetwork.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data format.";
        private const string DuplicatedDataMessage = "Duplicated data.";
        private const string SuccessfullyImportedMessageEntity = "Successfully imported message (Sent at: {0}, Status: {1})";
        private const string SuccessfullyImportedPostEntity = "Successfully imported post (Creator {0}, Created at: {1})";

        public static string ImportMessages(SocialNetworkDbContext dbContext, string xmlString)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<ImportMessageDto>? messageDtos
                = XmlSerializerWrapper.Deserialize<ImportMessageDto[]>(xmlString, "Messages");

            if (messageDtos == null)
            {
                return sb.ToString();
            }

            IEnumerable<Message> existingMessages = dbContext.Messages.AsNoTracking().ToArray();

            ICollection<Message> messagesToPersist = new List<Message>();

            foreach (ImportMessageDto messageDto in messageDtos)
            {
                if (!IsValid(messageDto)
                    || !Enum.TryParse(messageDto.Status, out Status parsedStatus)
                    || !dbContext.Conversations.Any(c => c.Id == messageDto.ConversationId)
                    || !dbContext.Users.Any(u => u.Id == messageDto.SenderId))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isDuplicate = existingMessages.Any(
                    m => m.Content == messageDto.Content 
                    && m.SentAt == messageDto.SentAt 
                    && m.Status == parsedStatus 
                    && m.ConversationId == messageDto.ConversationId 
                    && m.SenderId == messageDto.SenderId);

                isDuplicate = isDuplicate || messagesToPersist.Any(
                    m => m.Content == messageDto.Content
                    && m.SentAt == messageDto.SentAt
                    && m.Status == parsedStatus
                    && m.ConversationId == messageDto.ConversationId
                    && m.SenderId == messageDto.SenderId);

                if (isDuplicate)
                {
                    sb.AppendLine(DuplicatedDataMessage);
                    continue;
                }

                Message message = new Message()
                {
                    Content = messageDto.Content,
                    SentAt = messageDto.SentAt,
                    Status = parsedStatus,
                    ConversationId = messageDto.ConversationId,
                    SenderId = messageDto.SenderId
                };

                messagesToPersist.Add(message);

                string successMessage = string.Format(SuccessfullyImportedMessageEntity,
                    message.SentAt.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture),
                    message.Status);

                sb.AppendLine(successMessage);
            }

            dbContext.Messages.AddRange(messagesToPersist);
            dbContext.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPosts(SocialNetworkDbContext dbContext, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable<ImportPostDto>? postDtos
                = JsonConvert.DeserializeObject<ImportPostDto[]>(jsonString);

            if (postDtos == null)
            {
                return sb.ToString();
            }

            IEnumerable<Post> existingPosts = dbContext.Posts.AsNoTracking().ToArray();

            ICollection<Post> postsToPersist = new List<Post>();

            foreach (ImportPostDto postDto in postDtos)
            {
                if (!IsValid(postDto)
                    || !dbContext.Users.Any(u => u.Id == postDto.CreatorId))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isCreatedAtValidFormat = DateTime.TryParseExact(postDto.CreatedAt,
                    "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime postCreatedAt);

                if (!isCreatedAtValidFormat)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isDuplicate = existingPosts.Any(
                    p => p.Content == postDto.Content
                    && p.CreatedAt == postCreatedAt
                    && p.CreatorId == postDto.CreatorId);

                isDuplicate = isDuplicate || postsToPersist.Any(
                    p => p.Content == postDto.Content
                    && p.CreatedAt == postCreatedAt
                    && p.CreatorId == postDto.CreatorId);

                if (isDuplicate)
                {
                    sb.AppendLine(DuplicatedDataMessage);
                    continue;
                }

                Post post = new Post()
                {
                    Content = postDto.Content,
                    CreatedAt = postCreatedAt,
                    CreatorId = postDto.CreatorId
                };

                postsToPersist.Add(post);

                string successMessage = string.Format(SuccessfullyImportedPostEntity,
                    dbContext.Users.First(u => u.Id == post.CreatorId).Username,
                    post.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture));

                sb.AppendLine(successMessage);
            }

            dbContext.Posts.AddRange(postsToPersist);
            dbContext.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static bool IsValid(object dto)
        {
            ValidationContext validationContext = new ValidationContext(dto);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            foreach (ValidationResult validationResult in validationResults)
            {
                if (validationResult.ErrorMessage != null)
                {
                    string currentMessage = validationResult.ErrorMessage;
                }
            }

            return isValid;
        }
    }
}
