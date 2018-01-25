using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JecoreDotNetCommon.Metadata.Type
{

    /// <summary>
    /// 通过 静态泛型方法 简化 泛型类型的创建
    /// 让编译器 隐式类型推断
    /// </summary>
    public static class TypeMetadataProduct
    {
        public static ODataTypeMetadataProduct<TBrowse, TCongener, TOther, TImgother,TImage, TProductPrams, TProductInf>
            Of<TBrowse, TCongener, TOther, TImgother,TImage, TProductPrams, TProductInf>
            (TBrowse browse, TCongener congener, TOther other,
           TImgother imgother, TImage image, TProductPrams productPrams, TProductInf productInf) 
        {
            return new ODataTypeMetadataProduct<TBrowse, TCongener, TOther, TImgother, TImage, TProductPrams, TProductInf>
                (browse, congener, other, imgother, image, productPrams, productInf);
        }
    }

    public class ODataTypeMetadataProduct<TBrowse, TCongener, TOther, TImgother,
        TImage, TProductPrams, TProductInf>
    {
        public TBrowse Browse { private set; get; }
        public TCongener Congener { private set; get; }
        public TOther Other { private set; get; }
        public TImgother Imgother { private set; get; }
        public TImage Image { private set; get; }
        public TProductPrams ProductPrams { private set; get; }
        public TProductInf ProductInf { private set; get; }

        public ODataTypeMetadataProduct(TBrowse browse, TCongener congener, TOther other,
           TImgother imgother, TImage image, TProductPrams productPrams, TProductInf productInf)
        {
            this.Browse = browse;
            this.Congener = congener;
            this.Other = other;
            this.Imgother = imgother;
            this.Image = image;
            this.ProductPrams = productPrams;
            this.ProductInf = productInf;
        }
    }
}
