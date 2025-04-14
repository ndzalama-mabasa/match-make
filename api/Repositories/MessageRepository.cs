using Dapper;
using galaxy_match_make.Models;
using System.Data;

namespace galaxy_match_make.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly IDbConnection _dbConnection;

    public MessageRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<MessageDto>> GetAllMessagesAsync()
    {
        const string query = "SELECT * FROM messages";
        return await _dbConnection.QueryAsync<MessageDto>(query);
    }

    public async Task<MessageDto?> GetMessageByIdAsync(int id)
    {
        const string query = "SELECT * FROM messages WHERE id = @Id";
        return await _dbConnection.QuerySingleOrDefaultAsync<MessageDto>(query, new { Id = id });
    }

    public async Task AddMessageAsync(MessageDto message)
    {
        const string query = "INSERT INTO messages (message_content, sent_date, sender_id, recipient_id) VALUES (@MessageContent, @SentDate, @SenderId, @RecipientId)";
        await _dbConnection.ExecuteAsync(query, message);
    }

    public async Task UpdateMessageAsync(MessageDto message)
    {
        const string query = "UPDATE messages SET message_content = @MessageContent, sent_date = @SentDate, sender_id = @SenderId, recipient_id = @RecipientId WHERE id = @Id";
        await _dbConnection.ExecuteAsync(query, message);
    }

    public async Task DeleteMessageAsync(int id)
    {
        const string query = "DELETE FROM messages WHERE id = @Id";
        await _dbConnection.ExecuteAsync(query, new { Id = id });
    }

    public async Task<IEnumerable<MessageDto>> GetMessagesBetweenTwoUsers(Guid senderId, Guid receiverId)
    {
        const string query = "SELECT id, message_content, sent_date, sender_id, recipient_id FROM messages WHERE (sender_id = @SenderId AND recipient_id=@ReceiverId) OR (sender_id = @ReceiverId AND recipient_id=@SenderId)";

        return await _dbConnection.QueryAsync<MessageDto>(query, new { SenderId = senderId, ReceiverId = receiverId });
    }

    public async Task<List<ContactDto>> GetChatsByUserIdAsync(Guid userId)
    {
        const string query = @"
            SELECT DISTINCT
                p.user_id,
                p.display_name,
                p.avatar_url
            FROM messages m
            JOIN profiles p ON 
                p.user_id = CASE 
                    WHEN m.sender_id = @UserId THEN m.recipient_id 
                    ELSE m.sender_id 
                END
            WHERE 
                m.sender_id = @UserId OR m.recipient_id = @UserId";
    
        var allMessages = await _dbConnection.QueryAsync<ContactDto>(query, new { UserId = userId });
    
        return allMessages.ToList();
    }

}