using NoteAppBackend.DomainModels.DataTransferObjects;
using NoteAppBackend.Kernel.Common;

namespace NoteAppBackend.DomainModels;

public sealed class NoteType : BaseEntity
{
    private NoteType(Guid? id) : base(id ?? Ulid.NewUlid().ToGuid()) { }

    private NoteType() { }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ColorCode { get; set; } = string.Empty;

    public static NoteType Create(NoteTypeSummaryDto dto)
    {
        return new NoteType
        {
            Id = dto.TypeId,
            Name = dto.TypeName,
            Description = dto.Description,
            ColorCode = dto.TypeColorCode,
            CreatedAt = TimeOnly.FromDateTime(DateTimeOffset.UtcNow.LocalDateTime),
            CreatedOn = DateOnly.FromDateTime(DateTimeOffset.UtcNow.LocalDateTime)
        };
    }

    public static NoteType Create(NoteTypeCreationDto dto)
    {
        return new NoteType
        {
            Name = dto.TypeName,
            Description = dto.TypeDescription,
            ColorCode = dto.ColorCode,
            CreatedAt = TimeOnly.FromDateTime(DateTimeOffset.UtcNow.LocalDateTime),
            CreatedOn = DateOnly.FromDateTime(DateTimeOffset.UtcNow.LocalDateTime)
        };
    }
}
