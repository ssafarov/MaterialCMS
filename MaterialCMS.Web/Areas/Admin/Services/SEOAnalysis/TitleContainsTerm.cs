﻿using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using MaterialCMS.Entities.Documents.Web;
using MaterialCMS.Helpers;
using MaterialCMS.Web.Areas.Admin.Helpers;
using MaterialCMS.Web.Areas.Admin.Models.SEOAnalysis;

namespace MaterialCMS.Web.Areas.Admin.Services.SEOAnalysis
{
    public class TitleContainsTerm : BaseSEOAnalysisFacetProvider
    {
        public override IEnumerable<SEOAnalysisFacet> GetFacets(Webpage webpage, HtmlNode document, string analysisTerm)
        {
            string titleText = document.GetElementText("title");
            if (titleText != null && titleText.Contains(analysisTerm, StringComparison.OrdinalIgnoreCase))
            {
                if (titleText.StartsWith(analysisTerm))
                    yield return
                        GetFacet("Title contains term", SEOAnalysisStatus.Success,
                            string.Format("The title starts with the term '{0}', which is considered to improve rankings", analysisTerm));
                else
                {
                    yield return
                        GetFacet("Title contains term", SEOAnalysisStatus.CanBeImproved,
                            string.Format(
                                "The title contains the term '{0}'. To improve this, consider moving it to the start of the title, as this is considered to improve rankings",
                                analysisTerm));
                }
            }
            else
                yield return
                    GetFacet("Title contains term", SEOAnalysisStatus.Error,
                        string.Format("The title does not contain '{0}'", analysisTerm));
        }
    }
}