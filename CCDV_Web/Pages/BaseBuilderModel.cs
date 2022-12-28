// copyright (c) 2022 Roberto Ceccarelli - Casasoft
// http://strawberryfield.altervista.org 
// 
// This file is part of Casasoft Contemporary Carte de Visite Web
// https://github.com/strawberryfield/CCDV_Web
// 
// Casasoft CCDV Web is free software: 
// you can redistribute it and/or modify it
// under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Casasoft CCDV Web is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
// See the GNU General Public License for more details.
// 
// You should have received a copy of the GNU AGPL v.3
// along with Casasoft CCDV Web.  
// If not, see <http://www.gnu.org/licenses/>.

using Casasoft.CCDV;
using Casasoft.CCDV.Engines;
using Casasoft.CCDV.JSON;
using Microsoft.AspNetCore.Mvc;

namespace Casasoft.CCDV_Web.Pages;

public class BaseBuilderModel : CommonModel
{
    public BaseBuilderModel(IWebHostEnvironment environment, IConfiguration configuration)
       : base(environment, configuration)
    {
        PaperFormat = "Medium";
        Thickness = 5;
        isHorizontal = "Vertical";
        TargetFormat = "CDV";
        BorderText = "";
        FontFace = "Arial";
    }

    [BindProperty]
    public IFormFile FrontImage { get; set; }
    [BindProperty]
    public IFormFile BackImage { get; set; }
    [BindProperty]
    public IFormFile LeftImage { get; set; }
    [BindProperty]
    public IFormFile RightImage { get; set; }
    [BindProperty]
    public IFormFile TopImage { get; set; }
    [BindProperty]
    public IFormFile BottomImage { get; set; }

    [BindProperty]
    public bool UseTestImages { get; set; }
    [BindProperty]
    public string PaperFormat { get; set; }
    [BindProperty]
    public int Thickness { get; set; }
    [BindProperty]
    public string BorderText { get; set; }
    [BindProperty]
    public string TargetFormat { get; set; }
    [BindProperty]
    public string isHorizontal { get; set; }
    [BindProperty]
    public string FontFace { get; set; }
    [BindProperty]
    public bool FontBold { get; set; }
    [BindProperty]
    public bool FontItalic { get; set; }

    protected override async Task ReadData()
    {
        BaseBuilderParameters p = new();
        par = p;
        await base.ReadData();
        p.PaperFormat = Utils.GetPaperFormat(PaperFormat, PaperFormats.Medium);
        p.frontImage = LoadFile(FrontImage);
        p.backImage = LoadFile(BackImage);
        p.leftImage = LoadFile(LeftImage);
        p.rightImage = LoadFile(RightImage);
        p.topImage = LoadFile(TopImage);
        p.bottomImage = LoadFile(BottomImage);
        p.useTestImages = UseTestImages;
        p.spessore = Thickness;
        p.borderText = BorderText;
        p.targetFormat = TargetFormat == "CDV" ? 0 : 1;
        p.isHorizontal = isHorizontal == "Horizontal";
    }

    protected void setBuilderParameters()
    {
        if (engine is null || par is null)
            return;

        // Builders need to pass parameters directly
        BaseBuilderEngine eng = (BaseBuilderEngine)engine;
        BaseBuilder builder = (BaseBuilder)eng.Builder;
        BaseBuilderParameters p = (BaseBuilderParameters)par;
        builder.fillColor = GetColor(p.FillColor);
        builder.borderColor = GetColor(p.BorderColor);
        builder.fmt = eng.fmt;
        builder.borderText = p.borderText;
        builder.font = p.font;
        builder.fontBold = p.fontBold;
        builder.fontItalic = p.fontItalic;
        builder.isHorizontal = p.isHorizontal;
        builder.targetType = (TargetType)p.targetFormat;
        builder.PaperFormat = p.PaperFormat;
        builder.Thickness = p.spessore;

        builder.makeEmptyImages();
        if (p.useTestImages) builder.CreateTestImages();
        builder.SetFrontImage(p.frontImage);
        builder.SetBackImage(p.backImage);
        builder.SetTopImage(p.topImage);
        builder.SetBottomImage(p.bottomImage);
        builder.SetLeftImage(p.leftImage);
        builder.SetRightImage(p.rightImage);
    }

}
