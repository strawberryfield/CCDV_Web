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

using Casasoft.CCDV.Engines;
using Casasoft.CCDV.JSON;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Casasoft.CCDV_Web.Pages;

[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
public class MontaggioFotoModel : CommonModel
{
    public MontaggioFotoModel(IWebHostEnvironment environment, IConfiguration configuration)
        : base(environment, configuration)
    {
    }

    [BindProperty]
    public bool FullSize { get; set; }
    [BindProperty]
    public bool Trim { get; set; }
    [BindProperty]
    public bool Borders { get; set; }
    [BindProperty]
    public int Border { get; set; }

    public async Task OnPostAsync()
    {
        await ReadData();
        if (!string.IsNullOrWhiteSpace(FillColor))
        {
            ImgBase64 = DoWork();
        }
    }

    protected override async Task ReadData()
    {
        par = new MontaggioFotoParameters();
        await base.ReadData();
        MontaggioFotoParameters p = (MontaggioFotoParameters)par;
        p.FullSize = FullSize;
        p.Trim = Trim;
        p.WithBorder = Borders;
        p.Padding = Border;
    }

    protected override string DoWork()
    {
        if (par is null)
            return string.Empty;

        MontaggioFotoEngine eng = new();
        engine = eng;
        eng.SetJsonParams(JsonSerializer.Serialize(par));
        return base.DoWork();
    }

}
