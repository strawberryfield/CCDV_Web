// copyright (c) 2022-2023 Roberto Ceccarelli - Casasoft
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

namespace Casasoft.CCDV_Web.Pages;

[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
public class CartellaModel : BaseBuilderModel
{
    public CartellaModel(IWebHostEnvironment environment, IConfiguration configuration)
           : base(environment, configuration)
    {
    }

    public async Task OnPostAsync()
    {
        await ReadData();
        if (!string.IsNullOrWhiteSpace(FillColor))
        {
            ImgBase64 = DoWork();
        }
    }

    protected override string DoWork()
    {
        if (par is null)
            return string.Empty;

        FolderEngine eng = new();
        engine = eng;
        eng.SetJsonParams((BaseBuilderParameters)par);
        setBuilderParameters();
        return base.DoWork();
    }
}
