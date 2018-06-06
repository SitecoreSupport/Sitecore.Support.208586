using Sitecore.Links;
using Sitecore.Modules.EmailCampaign.Core;
using Sitecore.Modules.EmailCampaign.Core.Links;
using Sitecore.Modules.EmailCampaign.Core.Pipelines.GenerateLink;
using Sitecore.Sites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Sitecore.Support.Modules.EmailCampaign.Core.Pipelines.GenerateLink
{
  public class ExpandDynamicLink : GenerateLinkProcessor
  {
    public override void Process(GenerateLinkPipelineArgs args)
    {
      if (!LinksManager.IsAbsoluteLink(args.Url)&& !Regex.IsMatch(args.Url, "^([a-zA-Z0-9+.-]+:)"))
      {
        DynamicLink link;
        if ((args.Url.IndexOf("~/link.aspx?", StringComparison.InvariantCulture) >= 0) && DynamicLink.TryParse(args.Url, out link))
        {
          UrlOptions defaultUrlOptions = LinkManager.GetDefaultUrlOptions();
          defaultUrlOptions.SiteResolving = true;
          defaultUrlOptions.Site = SiteContext.GetSite(args.WebsiteConfigurationName);
          args.Url = LinkManager.GetItemUrl(new ItemUtilExt().GetItem(link.ItemId), defaultUrlOptions);
        }
      }
    }
  }
}