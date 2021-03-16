using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SoundClash.Data;
using SoundClash.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SoundClash.Authorization
{
    /// <summary>
    /// AuthorizationHandler checking whether User is Uploader
    /// </summary>
    public class UserIsOwnerAuthorizationHandler
        : AuthorizationHandler<OperationAuthorizationRequirement, int>
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly ApplicationDbContext _context;

        public UserIsOwnerAuthorizationHandler(UserManager<ApplicationUser>
            userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        protected override async Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement, int resourceID)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            // If not asking for CRUD permission, return.

            if (requirement.Name != Constants.CreateOperationName &&
                requirement.Name != Constants.ReadOperationName &&
                requirement.Name != Constants.UpdateOperationName &&
                requirement.Name != Constants.DeleteOperationName)
            {
                return Task.CompletedTask;
            }

            string soundUploaderId = await _context.Sound.Where(s => s.Id == resourceID)?.Select(s => s.Uploader.Id).SingleAsync();

            if (soundUploaderId == null)
            {
                return Task.CompletedTask;
            }
            
            if (soundUploaderId == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
