using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uCondition.Interfaces;
using uCondition.Models;
using Umbraco.Web;
using Umbraco.Web.Security;

namespace uCondition.Predicates.Members
{
    /// <summary>
    /// Predicate for checking whether the user is logged in
    /// </summary>
    public class MemberIsLoggedIn : Predicate
    {
        public MemberIsLoggedIn()
        {
            Name = "Member is logged in";
            Alias = "uCondition.Predicates.MemberIsLoggedIn";
            Icon = "icon-user";
            Category = "Member";
            Fields = new List<EditableProperty>();
        }

        public override bool Validate(IFieldValues fieldValues)
        {
            var membershipHelper = new MembershipHelper(UmbracoContext.Current);
            return membershipHelper.IsLoggedIn();
        }
    }
}
