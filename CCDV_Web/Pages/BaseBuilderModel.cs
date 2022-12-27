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
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
namespace Casasoft.CCDV_Web.Pages;

public class BaseBuilderModel : CommonModel
{
    public BaseBuilderModel(IWebHostEnvironment environment, IConfiguration configuration)
       : base(environment, configuration)
    {
        PaperFormat = "Medium";
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
    }

}
