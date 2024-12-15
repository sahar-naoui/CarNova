using backend.Models;
using backend.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        // Récupérer toutes les reviews d'un produit spécifique
        [HttpGet("product/{productId}")]
        public IActionResult GetReviewsByProduct(int productId)
        {
            var reviews = _reviewRepository.GetReviewsByProductId(productId);
            return Ok(reviews);
        }

        // Récupérer une review par ID
        [HttpGet("{id}")]
        public IActionResult GetReview(int id)
        {
            var review = _reviewRepository.GetReviewById(id);
            if (review == null)
                return NotFound("Avis non trouvé.");

            return Ok(review);
        }

        // Ajouter une nouvelle review
        [HttpPost]
        public IActionResult AddReview([FromBody] Review review)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _reviewRepository.AddReview(review);
            return CreatedAtAction(nameof(GetReview), new { id = review.Id }, review);
        }

        // Mettre à jour une review existante
        [HttpPut("{id}")]
        public IActionResult UpdateReview(int id, [FromBody] Review review)
        {
            if (id != review.Id)
                return BadRequest("ID mismatch.");

            var existingReview = _reviewRepository.GetReviewById(id);
            if (existingReview == null)
                return NotFound("Avis non trouvé.");

            _reviewRepository.UpdateReview(review);
            return NoContent();
        }

        // Supprimer une review par ID
        [HttpDelete("{id}")]
        public IActionResult DeleteReview(int id)
        {
            var existingReview = _reviewRepository.GetReviewById(id);
            if (existingReview == null)
                return NotFound("Avis non trouvé.");

            _reviewRepository.DeleteReview(id);
            return NoContent();
        }
    }
}
