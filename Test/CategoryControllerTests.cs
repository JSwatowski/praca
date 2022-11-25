using inzynierka.Controllers;
using inzynierka.Data.Base;
using inzynierka.Data.Services;
using inzynierka.Models;
using inzynierkav2.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using pracainz.Data;
using pracainz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test
{
    public class CategoryControllerTests
    {

        private readonly Mock<ICategoriesService> _mockRepo;
        private readonly CategoriesController _controller;


        public CategoryControllerTests()
        {
            _mockRepo = new Mock<ICategoriesService>();
            _controller = new CategoriesController(_mockRepo.Object);
        }

        private List<Category> GetCategoris()
        {
            var listMovie_Category = new List<Movie_Category>();
            var list = new List<Category>();
            list.Add(new Category()
            {
                Id = 1,
                Name = "drama",
                Movie_Categories = listMovie_Category
            });
            list.Add(new Category()
            {
                Id = 1,
                Name = "drama",
                Movie_Categories = listMovie_Category
            });
            return list;
        }

        [Fact]
        public async Task Index_Category_Returns_List()
        {
            _mockRepo.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(GetCategoris());

            var result = await _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Category>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Create_Category_CalledOnce_When_Valid()
        {
            var listMovie_Category = new List<Movie_Category>();

            Category cat = null;
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Category>()))
                .Callback<Category>(x => cat = x);
            var newCategory = new Category
            {
                Name = "dramae",
                Id = 1,
                Movie_Categories = listMovie_Category
            };
            _controller.Create(newCategory);
            _mockRepo.Verify(x => x.AddAsync(It.IsAny<Category>()), Times.Once);
            Assert.Equal(cat.Name, newCategory.Name);
            Assert.Equal(cat.Id, newCategory.Id);
            Assert.Equal(cat.Movie_Categories, newCategory.Movie_Categories);
        }

        [Fact]
        public void Create_Category_Bad_Model_Never_Exe()
        {
            _controller.ModelState.AddModelError("Name", "Name is required");
             var category = new Category { Name = "drama" };

            _controller.Create(category);

            _mockRepo.Verify(x => x.AddAsync(It.IsAny<Category>()), Times.Never);
        }

   
        [Fact]
        public void Create_Category_Return_View()
        {
            var result = _controller.Create();

            Assert.IsType<ViewResult>(result);
        }
    }
}
