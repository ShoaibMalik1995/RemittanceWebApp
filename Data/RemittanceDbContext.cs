using Microsoft.EntityFrameworkCore;
using RemittanceWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemittanceWebApp.Data
{
    public class RemittanceDbContext : DbContext
    {
        public RemittanceDbContext(DbContextOptions<RemittanceDbContext> options) : base(options)
        {

        }

        public DbSet<CardRequestLog> CardRequestLog { get; set; }
        public DbSet<CardTopupLog> CardTopupLog { get; set; }
        public DbSet<CustomerRegisterationLog> CustomerRegisterationLog { get; set; }
        public DbSet<FCRequestLog> FCRequestLog { get; set; }
        public DbSet<OTPSendLog> OTPSendLog { get; set; }
        public DbSet<OTPValidateLog> OTPValidateLog { get; set; }
        public DbSet<RemittanceApproveLog> RemittanceApproveLog { get; set; }
        public DbSet<RemittanceLog> RemittanceLog { get; set; }
        public DbSet<SICreateLog> SICreateLog { get; set; }
        public DbSet<BeneficiaryLog> BeneficiaryLog { get; set; }

    }
}
