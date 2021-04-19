using BlogLab.Models.Account;
using BlogLab.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlogLab.Identity
{
    public class UserStore :
        IUserStore<ApplicationUserIdentity>,
        IUserEmailStore<ApplicationUserIdentity>,
        IUserPasswordStore<ApplicationUserIdentity>
    {
        private readonly IAccountRepository _accountRepository;

        public UserStore(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            return await _accountRepository.CreateAsync(user, cancellationToken);
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            // Nothing to Dispose
        }

        public async Task<ApplicationUserIdentity> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUserIdentity> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUserIdentity> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await _accountRepository.GetByUsernameAsync(normalizedUserName, cancellationToken);
        }

        public async Task<string> GetEmailAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Email);
        }

        public async Task<bool> GetEmailConfirmedAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(true);
        }

        public async Task<string> GetNormalizedEmailAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.NormalizedEmail);
        }

        public async Task<string> GetNormalizedUserNameAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.NormalizedUsername);
        }

        public async Task<string> GetPasswordHashAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.PasswordHash);
        }

        public async Task<string> GetUserIdAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.ApplicationUserId.ToString());
        }

        public async Task<string> GetUserNameAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Username);
        }

        public async Task<bool> HasPasswordAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.PasswordHash != null);
        }

        public async Task SetEmailAsync(ApplicationUserIdentity user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            await Task.FromResult(0);
        }

        public async Task SetEmailConfirmedAsync(ApplicationUserIdentity user, bool confirmed, CancellationToken cancellationToken)
        {
            await Task.FromResult(0);
        }

        public async Task SetNormalizedEmailAsync(ApplicationUserIdentity user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            await Task.FromResult(0);
        }

        public async Task SetNormalizedUserNameAsync(ApplicationUserIdentity user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUsername = normalizedName;
            await Task.FromResult(0);
        }

        public async Task SetPasswordHashAsync(ApplicationUserIdentity user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            await Task.FromResult(0);
        }

        public async Task SetUserNameAsync(ApplicationUserIdentity user, string userName, CancellationToken cancellationToken)
        {
            user.Username = userName;
            await Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
