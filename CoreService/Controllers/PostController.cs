using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreService.Models;
using CoreService.ViewModel;
using CoreService.Repository;

namespace CoreService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {


        IPostRepository postRepository;
        public PostController(IPostRepository _postRepository)
        {
            postRepository = _postRepository;
        }

        [HttpGet]
        [Route("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await postRepository.GetCategories();
            if (categories == null)
            {
                return BadRequest();
            }

            return Ok(categories);
        }

        [HttpGet]
        [Route("GetPosts")]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await postRepository.GetPosts();
            if (posts == null)
            {
                return BadRequest();
            }

            return Ok(posts);
        }

        [HttpGet]
        [Route("GetPost")]
        public async Task<IActionResult> GetPost(int? postId)
        {
            if (postId == null)
            {
                return NotFound();
            }

            var post = await postRepository.GetPost(postId);
            if (post == null)
            {
                return BadRequest();
            }

            return Ok(post);
        }

        [HttpPost]
        [Route("AddPost")]
        public async Task<IActionResult> AddPost([FromBody]Post model)
        {
            if (ModelState.IsValid)
            {
                var postId = await postRepository.AddPost(model);
                if (postId > 0)
                {
                    return Ok(postId);
                }
                else
                {
                    return BadRequest();
                }
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("DeletePost")]
        public async Task<IActionResult> DeletePost(int? postId)
        {
            if (postId == null)
            {
                return NotFound();
            }

            await postRepository.DeletePost(postId);

            return Ok();
        }

        [HttpPost]
        [Route("UpdatePost")]
        public async Task<IActionResult> UpdatePost([FromBody]Post model)
        {
            if (ModelState.IsValid)
            {
                await postRepository.UpdatePost(model);
                return Ok();
            }

            return BadRequest();
        }
    }
}