namespace datasheetapi.Adapters;
public static class TagDataReviewAdapter
{
    public static ReviewStatusDto MapReviewStatusModelToDto(this ReviewStateEnum model)
    {
        return model switch
        {
            ReviewStateEnum.New => ReviewStatusDto.New,
            ReviewStateEnum.Reviewed => ReviewStatusDto.Reviewed,
            ReviewStateEnum.Resubmit => ReviewStatusDto.Resubmit,
            ReviewStateEnum.Diff => ReviewStatusDto.Diff,
            ReviewStateEnum.Duplicate => ReviewStatusDto.Duplicate,
            ReviewStateEnum.ReviewedWithComment => ReviewStatusDto.ReviewedWithComment,
            ReviewStateEnum.NotReviewed => ReviewStatusDto.NotReviewed,
            ReviewStateEnum.Deleted => ReviewStatusDto.Deleted,
            _ => throw new ArgumentOutOfRangeException(nameof(model), $"Unknown status: {model}"),
        };
    }

    public static ReviewStateEnum MapReviewStatusDtoToModel(this ReviewStatusDto dto)
    {
        return dto switch
        {
            ReviewStatusDto.New => ReviewStateEnum.New,
            ReviewStatusDto.Reviewed => ReviewStateEnum.Reviewed,
            ReviewStatusDto.Resubmit => ReviewStateEnum.Resubmit,
            ReviewStatusDto.Diff => ReviewStateEnum.Diff,
            ReviewStatusDto.Duplicate => ReviewStateEnum.Duplicate,
            ReviewStatusDto.ReviewedWithComment => ReviewStateEnum.ReviewedWithComment,
            ReviewStatusDto.NotReviewed => ReviewStateEnum.NotReviewed,
            ReviewStatusDto.Deleted => ReviewStateEnum.Deleted,
            _ => throw new ArgumentOutOfRangeException(nameof(dto), $"Unknown status: {dto}"),
        };
    }
}
