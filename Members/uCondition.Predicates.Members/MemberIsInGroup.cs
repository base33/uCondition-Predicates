using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using uCondition.Interfaces;
using uCondition.Models;
using Umbraco.Web;
using Umbraco.Web.Security;

namespace uCondition.Predicates.Members
{
    /// <summary>
    /// Predicate for checking whether the current member is in all/any member groups selected
    /// </summary>
    public class MemberIsInGroup : Predicate
    {
        public MemberIsInGroup()
        {
            Name = "Is member in group?";
            Alias = "uCondition.IsMemberInGroup";
            Icon = "icon-user";
            Category = "Member";
            Fields = new List<EditableProperty>
            {
                new EditableProperty("Group", "Group", "membergrouppicker"),
                //this data type will need to be added
                new EditableProperty("Member of all or any?", "MatchType", "uCondition type: all or any member group", EditablePropertyDisplayMode.IsPreValue)
            };
        }

        public override bool Validate(IFieldValues fieldValues)
        {
            var prevalueIdForAllOrAny = fieldValues.GetValue<int>("MatchType");
            var allOrAny = Umbraco.GetPreValueAsString(prevalueIdForAllOrAny);
            var membership = new MembershipHelper(UmbracoContext.Current);

            var groups = fieldValues.GetValue("Group").Split(',');
            bool success;
            if (allOrAny == "All")
            {
                success = groups.All(c => Roles.IsUserInRole(membership.CurrentUserName, c));
            }
            else
            {
                success = groups.Any(c => Roles.IsUserInRole(membership.CurrentUserName, c));
            }

            return membership.IsLoggedIn() && success;
        }
    }
}
