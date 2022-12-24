﻿// copyright (c) 2022 Roberto Ceccarelli - Casasoft
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
using ImageMagick;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Casasoft.CCDV_Web.Pages;

public class CommonModel : PageModel
{
    public void OnGet()
    {
    }

    protected IWebHostEnvironment _environment;
    protected IConfiguration _configuration;
    protected string TempPath;

    public CommonModel(IWebHostEnvironment environment, IConfiguration configuration)
    {
        _environment = environment;
        _configuration = configuration;
        TempPath = _configuration["CCDV_Web:TempPath"];
    }

    [BindProperty]
    public string? FillColor { get; set; }
    [BindProperty]
    public string? BorderColor { get; set; }
    [BindProperty]
    public int DPI { get; set; }
    [BindProperty]
    public List<IFormFile>? Upload { get; set; }
 
    protected IParameters? par;
    protected IEngine? engine;

    protected virtual async Task ReadData()
    {
        if (par is null) return;

        par.Dpi = DPI;
        par.BorderColor = BorderColor;
        par.FillColor = FillColor;
        if (Upload is not null)
        {
            foreach (var file in Upload)
            {
                string filename = Path.Combine(TempPath, file.FileName);
                par.FilesList.Add(filename);
                using (var fileStream = new FileStream(filename, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
        }
    }

    protected virtual void DoWork()
    {
        if (par is null || engine is null) return;

        string output = Path.Combine(_environment.WebRootPath, "results\\result.jpg");
        if (System.IO.File.Exists(output))
        {
            System.IO.File.Delete(output);
        }
        engine.SetJsonParams(JsonSerializer.Serialize(par));

        MagickImage img = engine.GetResult(true);
        img.Write(Path.Combine(_environment.WebRootPath, "results\\result.jpg"));
    }
}