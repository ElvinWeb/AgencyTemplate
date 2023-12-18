using Agency.Business.CustomExceptions.PortfolioExceptions;
using Agency.Business.Helpers;
using Agency.Core.Models;
using Agency.Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Business.Services.Implementations
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IWebHostEnvironment _env;
        public PortfolioService(IPortfolioRepository portfolioRepository, IWebHostEnvironment env)
        {
            _portfolioRepository = portfolioRepository;
            _env = env;
        }
        public async Task CreateAsync(Portfolio portfolio)
        {
            if (portfolio.ImgFile != null)
            {

                if (portfolio.ImgFile.ContentType != "image/png" && portfolio.ImgFile.ContentType != "image/jpeg")
                {
                    throw new InvalidContentTypeAndSize("ImgFile", "please select correct file type");
                }

                if (portfolio.ImgFile.Length > 1048576)
                {
                    throw new InvalidContentTypeAndSize("ImgFile", "file size should be more lower than 1mb");
                }
            }
            else
            {
                throw new InvalidImage("Image", "image file is must be choosed!! ");
            }

            string folder = "portfolio-images/";
            string newFileName = Helper.GetFileName(_env.WebRootPath, folder, portfolio.ImgFile);

            portfolio.ImgUrl = newFileName;


            await _portfolioRepository.CreateAsync(portfolio);
            await _portfolioRepository.CommitAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Portfolio>> GetAllAsync()
        {
            return await _portfolioRepository.GetAllAsync(x => x.IsDeleted == false, "Category");
        }

        public async Task<Portfolio> GetByIdAsync(int id)
        {
            Portfolio entity = await _portfolioRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false);

            if (entity is null) throw new NullReferenceException();

            return entity;
        }

        public IQueryable<Portfolio> GetPortfolioTable()
        {
            var query = _portfolioRepository.Table.AsQueryable();
            return query;
        }

        public async Task SoftDelete(int id)
        {
            if (id == null) throw new NullReferenceException();

            Portfolio wantedPortfolio = await _portfolioRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false);

            if (wantedPortfolio == null) throw new NullReferenceException();

            string fullPath = Path.Combine(_env.WebRootPath, "portfolio-images/", wantedPortfolio.ImgUrl);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            wantedPortfolio.IsDeleted = true;

            await _portfolioRepository.CommitAsync();
        }

        public async Task UpdateAsync(Portfolio portfolio)
        {
            Portfolio wantedPortfolio = await _portfolioRepository.GetByIdAsync(t => t.Id == portfolio.Id && t.IsDeleted == false);

            if (wantedPortfolio == null) throw new NullReferenceException();

            if (portfolio.ImgFile != null)
            {

                if (portfolio.ImgFile.ContentType != "image/png" && portfolio.ImgFile.ContentType != "image/jpeg")
                {

                    throw new InvalidContentTypeAndSize("ImgFile", "please select correct file type");
                }

                if (portfolio.ImgFile.Length > 1048576)
                {
                    throw new InvalidContentTypeAndSize("ImgFile", "file size should be more lower than 1mb");
                }

                string folderPath = "portfolio-images/";

                string newFileName = Helper.GetFileName(_env.WebRootPath, folderPath, portfolio.ImgFile);

                string wantedPath = Path.Combine(_env.WebRootPath, folderPath, wantedPortfolio.ImgUrl);

                if (File.Exists(wantedPath))
                {
                    File.Delete(wantedPath);
                }

                wantedPortfolio.ImgUrl = newFileName;
            }

            wantedPortfolio.Name = portfolio.Name;
            wantedPortfolio.Desc = portfolio.Desc;
            wantedPortfolio.Client = portfolio.Client;
            wantedPortfolio.SubTitle = portfolio.SubTitle;
            wantedPortfolio.CategoryId = portfolio.CategoryId;

            await _portfolioRepository.CommitAsync();
        }
    }
}
