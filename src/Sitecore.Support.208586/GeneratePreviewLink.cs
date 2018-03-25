using Sitecore.Diagnostics;
using Sitecore.Modules.EmailCampaign.Core.Links;
using Sitecore.Modules.EmailCampaign.Core.Pipelines.GenerateLink;
using System;
using System.Text;

namespace Sitecore.Support.Modules.EmailCampaign.Core.Pipelines.GenerateLink
{
  public class GeneratePreviewLink : GenerateLinkProcessor
  {
    public override void Process(GenerateLinkPipelineArgs args)
    {
      Assert.IsNotNull(args, "Arguments can't be null");
      Assert.IsNotNull(args.Url, "Url can't be null");
      Assert.IsNotNull(args.ServerUrl, "Server url link can't be null");
      if (!args.PreviewMode)
      {
        return;
      }
      StringBuilder stringBuilder = new StringBuilder();
      if (!LinksManager.IsAbsoluteLink(args.Url))
      {
        stringBuilder.Append(string.IsNullOrEmpty(args.MailMessage.ManagerRoot.Settings.PreviewBaseURL) ? args.ServerUrl : args.MailMessage.ManagerRoot.Settings.PreviewBaseURL);
      }
      string url = args.Url;

      if (url.IndexOf("/://") == 0)
      {
        url = url.Replace("/://", "");
        url = url.Substring(url.IndexOf("/") + 1);
      }
      else
          if (url.IndexOf("://") == 0)
      {
        url = url.Replace("://", "");
        url = url.Substring(url.IndexOf("/") + 1);
      }

      if (!url.StartsWith("/") && !stringBuilder.ToString().EndsWith("/"))
        url = "/" + url;

      stringBuilder.Append(url);
      args.GeneratedUrl = stringBuilder.ToString();
    }
  }
}
