using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FaceDlib.Core.Models
{
    public class CoinAppSettings
    {
        public DbConnection ConnectionStrings { get; }
        public AppSettings AppSettings { get; }
        public static CoinAppSettings Instance { get;private set; }
        //static CoinAppSettings()
        //{
        //    var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
        //    .AddJsonFile("appsettings.json").Build();
        //    Instance = new CoinAppSettings(builder);
        //}
        public static void CreateInstence(IConfigurationRoot builder)
        {
            Instance = new CoinAppSettings(builder);
        }
        public CoinAppSettings(IConfigurationRoot builder)
        {
            this.ConnectionStrings = new DbConnection(builder.GetSection("ConnectionStrings"));
            this.AppSettings = new AppSettings(builder.GetSection("AppSettings"));
        }
    }
    /// <summary>
    /// 数据库、redis、mq配置连接信息
    /// </summary>
    public class DbConnection
    {
        public string DapperRead { get;}
        public string DapperWrite { get;}
        public string RedisConnMain { get;}
        public string RedisConnVice { get;}
        public string RabbitMqHostName { get;}
        public string RabbitMqUserName { get;}
        public string RabbitMqPassword { get;}
        public string RedisConnSignalr { get; }
        public DbConnection(IConfigurationSection section)
        {
            this.DapperRead = section.GetSection("DapperRead").Value;
            this.DapperWrite = section.GetSection("DapperWrite").Value;

            this.RedisConnMain = section.GetSection("RedisConnMain").Value;
            this.RedisConnVice = section.GetSection("RedisConnVice").Value;

            this.RabbitMqHostName = section.GetSection("RabbitMqHostName").Value;
            this.RabbitMqUserName = section.GetSection("RabbitMqUserName").Value;
            this.RabbitMqPassword = section.GetSection("RabbitMqPassword").Value;

            this.RedisConnSignalr = section.GetSection("RedisConnSignalr").Value;
        }

    }

    public class AppSettings
    {
        public string OpenVerifyCode { get; }
        public string DefaultMobileCodeApi { get; }
        /// <summary>
        /// redis,mq key 的前缀
        /// </summary>
        public string RedisKePrefix { get;}
        /// <summary>
        /// 下单接口url
        /// </summary>
        public string ApiHost { get; }
        /// <summary>
        /// K线接口
        /// </summary>
        public string Apikline { get; }
        /// <summary>
        /// otc url
        /// </summary>
        public string OtcUrl { get; }
        public string MongoConn { get; }
        /// <summary>
        /// 集合ip接口
        /// </summary>
        public string JuHeIpKeyUrl { get;}
        /// <summary>
        /// 集合ip接口GUID
        /// </summary>
        public string JuHeIpKeyGuid { get; }
        /// <summary>
        /// 图片浏览路径
        /// </summary>
        public string ServerImgaes { get; }
        /// <summary>
        /// 图片保存路径
        /// </summary>
        public string SaveToImgaes { get; }
        public string EncryptionKey { get; }
        public string HashKey { get; }
        public string DefaultCulture { get; }
        public string GoogleTitle { get; }
        public string SEOKey { get; }
        public string Copyright { get; }
        public string BTCId { get; }
        public string BZId { get; }
        public string UsdtCoinId { get; }
        public string CertifiedVendorFreezeAmount { get; set; }
        public string DefaultMobieCodeCompany { get; set; }
        public string USDExchangeRate { get; }
        public string BitCoinUsdPrice { get; }
        public string SysStop { get; }
        public string DefaultVerifyCodeExpirationTimeSpan { get; }
        public string TransactionType { get; }
        public string CoinBit { get; }
        public string VisitTime { get; }
        public string VisitTimes { get; }
        public string DefaultCurrency { get; }
        public string RootDomain { get; }
        public string DesEncryptKey { get; }
        public string DefaultValidMinutes { get; }

        public string DefaultHelpClassId { get; }

        public string OTC_TradeDomain { get; }

        public string AddressByIPKey { get; }

        public string RtmpServer { get; }

        public string Version { get; }







        public AppSettings(IConfigurationSection section)
        {
            this.Version = section.GetSection("Version").Value;
            this.OpenVerifyCode = section.GetSection("OpenVerifyCode").Value;
            this.RedisKePrefix = section.GetSection("RedisKePrefix").Value;
            this.ApiHost = section.GetSection("ApiHost").Value;
            this.RtmpServer = section.GetSection("RtmpServer").Value;
        }
    }
}
