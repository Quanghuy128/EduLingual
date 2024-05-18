using System.ComponentModel.DataAnnotations.Schema;

namespace EduLingual.Domain.Dtos
{
    public record BaseDto
    (
        Guid Id,
        DateTime CreatedAt,
        DateTime UpdatedAt,
        string? CreatedBy,
        string? UpdatedBy,
        bool IsDeleted
    );
}
