﻿using BloodSearch.Core.Extensions;
using BloodSearch.Filter.Models.Offers;
using BloodSearch.Models.Api.Models.Offers;
using System.Linq;

namespace BloodSearch.Filter.Utils {

    public static class OffersQueryParamsUtils {

        public static ParseFilterResult ParseFilterFromQueryParameters(QueryParameters qParams) {

            var filter = new SearchFilter {
                Categories = qParams.Cat.SplitInt().Select(x => MapCategory(x)).ToList(),
                Region = qParams.Reg.ToPositiveInt() ?? 1,
                Sort = qParams.Sort.ToEnum(SearchFilter.SortEnum.Default),
                Type = qParams.Type.ToEnum(OfferTypeEnum.Donor)
            };

            filter.Statuses.Add(OfferStateEnum.Published);

            var maxRows = 2000;
            var pagingFilter = new PagingFilter();
            pagingFilter.PageSize = qParams.Ps.ToPositiveInt() ?? 20;
            pagingFilter.PageNumber = qParams.P.ToPositiveInt() ?? 1;
            pagingFilter.MaxPageNumber = maxRows / pagingFilter.PageSize;

            if (pagingFilter.PageNumber > pagingFilter.MaxPageNumber) {
                pagingFilter.PageNumber = pagingFilter.MaxPageNumber;
            }

            var result = new ParseFilterResult {
                Filter = filter,
                PagingFilter = pagingFilter
            };

            return result;
        }

        private static CategoryEnum MapCategory(int category) {

            switch (category) {
                case 1:
                    return CategoryEnum.FirstNegative;
                case 2:
                    return CategoryEnum.FirstPositive;
                case 3:
                    return CategoryEnum.SecondNegative;
                case 4:
                    return CategoryEnum.SecondPositive;
                case 5:
                    return CategoryEnum.ThirdNegative;
                case 6:
                    return CategoryEnum.ThirdPositive;
                case 7:
                    return CategoryEnum.FourthNegative;
                case 8:
                    return CategoryEnum.FourthPositive;
            }

            return 0;
        }
    }
}