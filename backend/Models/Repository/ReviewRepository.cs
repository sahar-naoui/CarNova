using backend.Models;
using backend.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly Context _context;

        public ReviewRepository(Context context)
        {
            _context = context;
        }

        // Récupérer toutes les reviews pour un produit spécifique
        public IEnumerable<Review> GetReviewsByProductId(int productId)
        {
            return _context.Reviews
                .Include(r => r.Customer)
                .Where(r => r.ProductId == productId)
                .ToList();
        }

        // Récupérer une review par son ID
        public Review GetReviewById(int id)
        {
            return _context.Reviews
                .Include(r => r.Customer)
                .FirstOrDefault(r => r.Id == id);
        }

        // Ajouter une review
        public void AddReview(Review review)
        {
            _context.Reviews.Add(review);
            Save();
        }

        // Mettre à jour une review existante
        public void UpdateReview(Review review)
        {
            var existingReview = _context.Reviews.Find(review.Id);
            if (existingReview != null)
            {
                _context.Entry(existingReview).CurrentValues.SetValues(review);
                Save();
            }
        }

        // Supprimer une review par ID
        public void DeleteReview(int id)
        {
            var review = _context.Reviews.Find(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                Save();
            }
        }

        // Sauvegarder les modifications
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
