﻿namespace _03._Road_Trip
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Item
    {
        public int Weight { get; set; }
        public int Value { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Item> items = new List<Item>();

            int[] values = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();
            int[] spaces = Console.ReadLine()
                .Split(", ")
                .Select(int.Parse)
                .ToArray();
            int maxCapacity = int.Parse(Console.ReadLine());

            for (int i = 0; i < values.Length; i++)
            {
                items.Add(new Item
                {
                    Weight = spaces[i],
                    Value = values[i],
                });
            }

            int[,] matrix = CreateMatrix(items, maxCapacity);
            var selectedItems = GetTakenItems(items, matrix);

            Console.WriteLine($"Maximum value: {selectedItems.Sum(x => x.Value)}");
        }

        private static List<Item> GetTakenItems(List<Item> items, int[,] matrix)
        {
            List<Item> selectedItems = new List<Item>();

            int index = matrix.GetLength(0) - 1;
            int capacity = matrix.GetLength(1) - 1;

            while (index > 0 && capacity > 0)
            {
                if (matrix[index, capacity] != matrix[index - 1, capacity])
                {
                    var item = items[index - 1];
                    selectedItems.Add(item);

                    capacity -= item.Weight;
                }

                index -= 1;
            }

            return selectedItems;
        }

        private static int[,] CreateMatrix(List<Item> items, int maxCapacity)
        {
            var matrix = new int[items.Count + 1, maxCapacity + 1];

            for (int index = 1; index < matrix.GetLength(0); index++)
            {
                var item = items[index - 1];

                for (int capacity = 1; capacity < matrix.GetLength(1); capacity++)
                {
                    var excluding = matrix[index - 1, capacity];
                    if (item.Weight > capacity)
                    {
                        matrix[index, capacity] = excluding;
                    }
                    else
                    {
                        var including = item.Value + matrix[index - 1, capacity - item.Weight];
                        matrix[index, capacity] = Math.Max(excluding, including);
                    }
                }
            }

            return matrix;
        }
    }
}
