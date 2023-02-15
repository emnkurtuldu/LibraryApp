using Microsoft.AspNetCore.Mvc;
using TimApp.Applicaiton.Dto;
using TimApp.Applicaiton.Interfaces.Repository;
using TimApp.Domain.Entities;
using TimApp.Persistence.Repositories;

namespace TimApp.WebUI.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberRepository _memberRepository;

        public MemberController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }
        public async Task<IActionResult> Index(int page=1)
        {
            var members = await _memberRepository.GetAllPagedAsync(page);
            ICollection<MemberDto> memberDtos = members.Select(s => new MemberDto()
            {
                Id = s.Id,
                MemberId = s.MemberId,
                Name = s.Name,
                Surname = s.Surname,
            }).ToList();

            return View(memberDtos);
        }

        [HttpPost]
        public async Task<IActionResult> AddMember(MemberDto memberDto)
        {
            var member = new Member()
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                MemberId = memberDto.MemberId,
                Surname = memberDto.Surname,    
                Name = memberDto.Name,
            };
            await _memberRepository.CreateAsync(member);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditMember(MemberDto memberDto)
        {
            var member = await _memberRepository.FindMemberFromIdAsync(memberDto.Id);
            member.MemberId = memberDto.MemberId;
            member.Name = memberDto.Name;
            member.Surname = memberDto.Surname;

            await _memberRepository.UpdateAsync(member);
            return RedirectToAction("Index");
        }
    }
}
