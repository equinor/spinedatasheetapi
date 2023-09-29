namespace datasheetapi.Dtos;

public enum ReviewStatusDto
{
    New = 0,
    Reviewed = 3,
    Resubmit = 4,
    Diff = 5,
    Duplicate = 6,
    ReviewedWithComment = 7,
    NotReviewed = 8,
    Deleted = 9,
}
