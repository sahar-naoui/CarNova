using System.Collections.Generic;

namespace backend.Models.Repository
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetReviewsByProductId(int productId);
        Review GetReviewById(int id);
        void AddReview(Review review);
        void UpdateReview(Review review);
        void DeleteReview(int id);
        void Save();
    }
}
