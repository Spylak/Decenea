using Decenea.Domain.Common;
using Decenea.Common.Common;

namespace Decenea.Domain.Aggregates.UserAggregate;

public abstract class RoleSmartEnum : SmartEnum<RoleSmartEnum>
{
    public static readonly RoleSmartEnum SuperAdmin = new SuperAdminRole();
    public static readonly RoleSmartEnum Admin = new AdminRole();
    public static readonly RoleSmartEnum Member = new MemberRole();
    public static readonly RoleSmartEnum Guest = new GuestRole();
    
    protected RoleSmartEnum(int value, string name)
        : base(value, name)
    {
    }

    public abstract bool Verified { get; }

    private sealed class SuperAdminRole : RoleSmartEnum
    {
        public SuperAdminRole()
            : base(1, "SuperAdmin")
        {
        }

        public override bool Verified => true;
    }

    private sealed class AdminRole : RoleSmartEnum
    {
        public AdminRole()
            : base(2, "Admin")
        {
        }

        public override bool Verified => true;
    }

    private sealed class MemberRole : RoleSmartEnum
    {
        public MemberRole()
            : base(3, "Member")
        {
        }

        public override bool Verified => true;
    }
    private sealed class GuestRole : RoleSmartEnum
    {
        public GuestRole()
            : base(3, "Guest")
        {
        }

        public override bool Verified => false;
    }
}
