namespace BusTicketSystem.Service.Contracts
{
    public interface IReviewService
    {
        string PrintReviews(int busCompanyId);
        void PublishReview(int customerId, double grade, int companyId, string content);
    }
}