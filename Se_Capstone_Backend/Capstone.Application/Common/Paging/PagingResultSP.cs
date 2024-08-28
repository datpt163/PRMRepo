﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Common.Paging
{
    public class PagingResultSP<T> : List<T>
    {
        public PagingSP Paging { get; set; }
        public IList<T> Data { get; set; }

        public PagingResultSP()
        {
        }

        public PagingResultSP(IList<T> data, int totalCount, int pageIndex, int pageSize)
        {
            Paging = new PagingSP(totalCount, pageIndex, pageSize);
            Data = data;
        }

        public PagingResultSP(IEnumerable<T> data, int totalCount, int pageIndex, int pageSize, bool isList)
        {
            Paging = new PagingSP(totalCount, pageIndex, pageSize);
            Data = data.ToList();
        }

        //public static async Task<PagingResultSP<T>> CreateAsyncSP(IQueryable<T> query, int pageIndex, int pageSize)
        //{
        //    var items = await query.ToListAsync();
        //    var count = items.FirstOrDefault()?.TotalRows ?? 0;

        //    return new PagingResultSP<T>(items, count, pageIndex, pageSize);
        //}

        public static async Task<PagingResultSP<T>> CreateAsyncLinq(IQueryable<T> query, int totalRow, int pageIndex, int pageSize)
        {
            var items = await query.ToListAsync();

            return new PagingResultSP<T>(items, totalRow, pageIndex, pageSize);
        }
    }
}