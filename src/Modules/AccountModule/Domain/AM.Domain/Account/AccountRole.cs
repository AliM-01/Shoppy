using _0_Framework.Domain.Attributes;
using AspNetCore.Identity.MongoDbCore.Models;

namespace AM.Domain.Account;

[BsonCollection("accountRoles")]
public class AccountRole : MongoIdentityRole<Guid>
{
}
