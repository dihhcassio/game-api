using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameAPI.Models;
using GameAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class FriendController : Controller
    {
        private readonly IFriendRepository _friendRepository;
        public FriendController(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        [HttpPost]
        [Route("")]
        public dynamic Insert([FromBody] Friend friend)
        {
            _friendRepository.Insert(friend);
            _friendRepository.Save();
            return new { sucess = true, id = friend.Id };
        }

        [HttpPut]
        [Route("")]
        public dynamic Update([FromBody] Friend friend)
        {
            _friendRepository.Update(friend);
            _friendRepository.Save();
            return new { sucess = true, id = friend.Id };
        }


        [HttpDelete]
        [Route("{id}")]
        public dynamic Delete(int id)
        {
            var friend = _friendRepository.Get(id);
            if (friend == null)
                return NotFound();
                
            _friendRepository.Delete(friend);
            _friendRepository.Save();
            return new { sucess = true };
        }

        [HttpGet]
        [Route("{id}")]
        public object Get(int id)
        {
            var friend = _friendRepository.Get(id);

            if (friend == null)
                return NotFound();

            return new
            {
                friend.Id,
                friend.Name,
                friend.Phone,
                CurrentLoan = friend.GameLoans.Where(gl => gl.Status == LentStatus.DELIVERED)
                    .Select(gl =>
                        new
                        {
                            gl.Status,
                            gl.DeliveredDate,
                            gl.ReceivedDate,
                            GameTitle = gl.Game.Title
                        })
                    .ToList()
            };
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<object> GetAll()
        {
            var friends = _friendRepository.GetAll();

            return friends.Select(f =>
            new
            {
                f.Id,
                f.Name,
                f.Phone,
                CurrentLoan = f.GameLoans.Where(gl => gl.Status == LentStatus.DELIVERED)
                    .Select(gl =>
                        new {
                            gl.Status,
                            gl.DeliveredDate,
                            gl.ReceivedDate,
                            GameTitle = gl.Game.Title
                        })
                    .ToList ()
            }).ToList();
        }

    }
}
