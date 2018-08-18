namespace KBCsv.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public sealed class RecordBaseFixture
    {
        [Fact]
        public void constructor_assigns_given_values()
        {
            var record = new ConcreteRecordBase("value1", "value2", "value3", "value4");
            Assert.Equal(4, record.Count);
            Assert.Equal("value1", record[0]);
            Assert.Equal("value2", record[1]);
            Assert.Equal("value3", record[2]);
            Assert.Equal("value4", record[3]);
        }

        [Fact]
        public void constructor_can_be_instructed_to_make_values_read_only()
        {
            var record = new ConcreteRecordBase(true, "value1", "value2", "value3", "value4");
            Assert.True(record.IsReadOnly);
            Assert.Throws<NotSupportedException>(() => record[0] = "new");
        }

        [Fact]
        public void indexer_get_throws_if_index_is_negative()
        {
            string ignore;
            var record = new ConcreteRecordBase();
            Assert.Throws<ArgumentOutOfRangeException>(() => ignore = record[-1]);
        }

        [Fact]
        public void indexer_get_throws_if_index_is_invalid()
        {
            string ignore;
            var record = new ConcreteRecordBase();
            Assert.Throws<ArgumentOutOfRangeException>(() => ignore = record[12]);
        }

        [Fact]
        public void indexer_get_retrieves_value_for_given_index()
        {
            var record = new ConcreteRecordBase("value1", "value2", "value3", "value4");
            Assert.Equal("value1", record[0]);
            Assert.Equal("value2", record[1]);
            Assert.Equal("value3", record[2]);
            Assert.Equal("value4", record[3]);
        }

        [Fact]
        public void indexer_set_throws_if_index_is_negative()
        {
            var record = new ConcreteRecordBase();
            Assert.Throws<ArgumentOutOfRangeException>(() => record[-1] = "test");
        }

        [Fact]
        public void indexer_set_throws_if_index_is_invalid()
        {
            var record = new ConcreteRecordBase();
            Assert.Throws<ArgumentOutOfRangeException>(() => record[12] = "test");
        }

        [Fact]
        public void indexer_set_throws_if_read_only()
        {
            var record = new ConcreteRecordBase(true, "one", "two");
            Assert.Throws<NotSupportedException>(() => record[0] = "test");
        }

        [Fact]
        public void indexer_set_throws_if_value_is_null()
        {
            var record = new ConcreteRecordBase("one", "two");
            Assert.Throws<ArgumentNullException>(() => record[0] = null);
        }

        [Fact]
        public void indexer_set_assigns_value_for_given_index()
        {
            var record = new ConcreteRecordBase("value1", "value2", "value3", "value4");
            record[0] += "Altered";
            record[1] += "Altered";
            record[2] += "Altered";
            record[3] += "Altered";
            Assert.Equal("value1Altered", record[0]);
            Assert.Equal("value2Altered", record[1]);
            Assert.Equal("value3Altered", record[2]);
            Assert.Equal("value4Altered", record[3]);
        }

        [Fact]
        public void is_read_only_returns_false_if_not_read_only()
        {
            var record = new ConcreteRecordBase();
            Assert.False(record.IsReadOnly);
        }

        [Fact]
        public void is_read_only_returns_true_if_read_only()
        {
            var record = new ConcreteRecordBase(true);
            Assert.True(record.IsReadOnly);
        }

        [Fact]
        public void count_returns_number_of_values()
        {
            var record1 = new ConcreteRecordBase("one", "two");
            Assert.Equal(2, record1.Count);

            var record2 = new ConcreteRecordBase(new string[] { "one", "two" }.ToList());
            record2.Add("three");
            Assert.Equal(3, record2.Count);
        }

        [Fact]
        public void get_value_or_null_returns_null_if_index_is_invalid()
        {
            var record = new ConcreteRecordBase();
            Assert.Null(record.GetValueOrNull(-1));
            Assert.Null(record.GetValueOrNull(1));
            Assert.Null(record.GetValueOrNull(10));
        }

        [Fact]
        public void get_value_or_null_returns_value_if_index_is_valid()
        {
            var record = new ConcreteRecordBase("value1", "value2", "value3", "value4");
            Assert.Equal("value1", record.GetValueOrNull(0));
            Assert.Equal("value2", record.GetValueOrNull(1));
            Assert.Equal("value3", record.GetValueOrNull(2));
            Assert.Equal("value4", record.GetValueOrNull(3));
        }

        [Fact]
        public void clear_throws_if_read_only()
        {
            var record = new ConcreteRecordBase(true, "one", "two");
            Assert.Throws<NotSupportedException>(() => record.Clear());
        }

        [Fact]
        public void clear_removes_all_values()
        {
            var record = new ConcreteRecordBase(new string[] { "one", "two" }.ToList());
            Assert.Equal(2, record.Count);
            record.Clear();
            Assert.Empty(record);
        }

        [Fact]
        public void contains_throws_if_value_is_null()
        {
            var record = new ConcreteRecordBase();
            Assert.Throws<ArgumentNullException>(() => record.Contains(null));
        }

        [Fact]
        public void contains_returns_false_if_value_is_not_in_record()
        {
            var record = new ConcreteRecordBase("one", "two");
            Assert.DoesNotContain("three", record);
            Assert.DoesNotContain("ONE", record);
            Assert.DoesNotContain("Two", record);
        }

        [Fact]
        public void contains_returns_true_if_value_is_in_record()
        {
            var record = new ConcreteRecordBase("one", "two");
            Assert.Contains("one", record);
            Assert.Contains("two", record);
        }

        [Fact]
        public void index_of_returns_negative_one_if_value_is_not_in_record()
        {
            var record = new ConcreteRecordBase("one", "two");
            Assert.Equal(-1, record.IndexOf("three"));
            Assert.Equal(-1, record.IndexOf("ONE"));
            Assert.Equal(-1, record.IndexOf("Two"));
        }

        [Fact]
        public void index_of_returns_index_if_value_is_in_record()
        {
            var record = new ConcreteRecordBase("one", "two", "three");
            Assert.Equal(0, record.IndexOf("one"));
            Assert.Equal(1, record.IndexOf("two"));
            Assert.Equal(2, record.IndexOf("three"));
        }

        [Fact]
        public void add_throws_if_read_only()
        {
            var record = new ConcreteRecordBase(true);
            Assert.Throws<NotSupportedException>(() => record.Add("one"));
        }

        [Fact]
        public void add_throws_if_value_is_null()
        {
            var record = new ConcreteRecordBase();
            Assert.Throws<ArgumentNullException>(() => record.Add(null));
        }

        [Fact]
        public void add_adds_specified_value()
        {
            var record = new ConcreteRecordBase();

            record.Add("one");
            Assert.Single(record);
            Assert.Contains("one", record);

            record.Add("two");
            Assert.Equal(2, record.Count);
            Assert.Contains("two", record);
        }

        [Fact]
        public void insert_throws_if_index_is_invalid()
        {
            var record = new ConcreteRecordBase();
            Assert.Throws<ArgumentOutOfRangeException>(() => record.Insert(-1, "one"));
            Assert.Throws<ArgumentOutOfRangeException>(() => record.Insert(10, "one"));
        }

        [Fact]
        public void insert_throws_if_read_only()
        {
            var record = new ConcreteRecordBase(true);
            Assert.Throws<NotSupportedException>(() => record.Insert(0, "one"));
        }

        [Fact]
        public void insert_throws_if_value_is_null()
        {
            var record = new ConcreteRecordBase();
            Assert.Throws<ArgumentNullException>(() => record.Insert(0, null));
        }

        [Fact]
        public void insert_inserts_value_at_specified_index()
        {
            var record = new ConcreteRecordBase("two", "four");
            record.Insert(1, "three");
            record.Insert(0, "one");
            Assert.Equal(4, record.Count);
            Assert.Equal("one", record[0]);
            Assert.Equal("two", record[1]);
            Assert.Equal("three", record[2]);
            Assert.Equal("four", record[3]);
        }

        [Fact]
        public void remove_throws_if_read_only()
        {
            var record = new ConcreteRecordBase(true, "one");
            Assert.Throws<NotSupportedException>(() => record.Remove("one"));
        }

        [Fact]
        public void remove_throws_if_value_is_null()
        {
            var record = new ConcreteRecordBase();
            Assert.Throws<ArgumentNullException>(() => record.Remove(null));
        }

        [Fact]
        public void remove_returns_false_if_value_not_removed()
        {
            var record = new ConcreteRecordBase("one", "two", "three");
            Assert.False(record.Remove("four"));
            Assert.False(record.Remove("One"));
        }

        [Fact]
        public void remove_returns_true_if_value_is_removed()
        {
            var record = new ConcreteRecordBase("one", "two", "three");
            Assert.True(record.Remove("one"));
            Assert.True(record.Remove("two"));
            Assert.True(record.Remove("three"));
        }

        [Fact]
        public void remove_removes_value()
        {
            var record = new ConcreteRecordBase("one", "two", "three");

            Assert.Contains("one", record);
            record.Remove("one");
            Assert.DoesNotContain("one", record);
            Assert.Equal(2, record.Count);
        }

        [Fact]
        public void remove_at_throws_if_index_is_invalid()
        {
            var record = new ConcreteRecordBase();
            Assert.Throws<ArgumentOutOfRangeException>(() => record.RemoveAt(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => record.RemoveAt(0));
        }

        [Fact]
        public void remove_at_throws_if_read_only()
        {
            var record = new ConcreteRecordBase(true, "one");
            Assert.Throws<NotSupportedException>(() => record.RemoveAt(0));
        }

        [Fact]
        public void copy_to_copies_values_to_array()
        {
            var record = new ConcreteRecordBase("one", "two", "three");
            var array = new string[record.Count];
            record.CopyTo(array, 0);
            Assert.Equal("one", array[0]);
            Assert.Equal("two", array[1]);
            Assert.Equal("three", array[2]);
        }

        [Fact]
        public void equals_returns_false_if_compared_to_null()
        {
            var record = new ConcreteRecordBase();
            Assert.False(record.Equals(null));
        }

        [Fact]
        public void equals_returns_true_if_compared_to_self()
        {
            var record = new ConcreteRecordBase();
            Assert.True(record.Equals(record));
        }

        [Fact]
        public void equals_returns_false_if_record_count_differs()
        {
            var record1 = new ConcreteRecordBase("first", "second");
            var record2 = new ConcreteRecordBase("first", "second", "");
            Assert.False(record1.Equals(record2));
        }

        [Fact]
        public void equals_returns_false_if_record_values_differ()
        {
            var record1 = new ConcreteRecordBase("first", "second");
            var record2 = new ConcreteRecordBase("first", "2nd");
            Assert.False(record1.Equals(record2));
        }

        [Fact]
        public void equals_returns_true_if_records_are_both_empty()
        {
            Assert.True(new ConcreteRecordBase().Equals(new ConcreteRecordBase()));
        }

        [Fact]
        public void equals_returns_true_if_record_values_are_equal()
        {
            var record1 = new ConcreteRecordBase("first", "second", "third");
            var record2 = new ConcreteRecordBase("first", "second", "third");
            Assert.True(record1.Equals(record2));
        }

        [Fact]
        public void equals_returns_true_if_record_values_are_equal_even_if_read_only_flag_differs()
        {
            var record1 = new ConcreteRecordBase("first", "second", "third");
            var record2 = new ConcreteRecordBase(true, "first", "second", "third");
            Assert.True(record1.Equals(record2));
        }

        [Fact]
        public void get_hash_code_returns_same_hash_for_equal_records()
        {
            var record1 = new ConcreteRecordBase("first", "second", "third");
            var record2 = new ConcreteRecordBase("first", "second", "third");

            Assert.Equal(record1.GetHashCode(), record2.GetHashCode());
        }

        [Fact]
        public void to_string_returns_a_useful_debug_representation_of_the_record()
        {
            var separator = (char)0x2022;
            var record = new ConcreteRecordBase("value1", "value2", "value3", "value4");
            Assert.Equal(string.Format("value1{0}value2{0}value3{0}value4{0}", separator), record.ToString());
            record[1] += " ";
            Assert.Equal(string.Format("value1{0}value2 {0}value3{0}value4{0}", separator), record.ToString());
            record.Add("a new value");
            Assert.Equal(string.Format("value1{0}value2 {0}value3{0}value4{0}a new value{0}", separator), record.ToString());
        }

        [Fact]
        public void record_is_enumerable()
        {
            var record = new ConcreteRecordBase("first", "second", "third");
            var values = new List<string>();

            foreach (var value in record)
            {
                values.Add(value);
            }

            Assert.Equal(3, values.Count);
            Assert.Contains("first", values);
            Assert.Contains("second", values);
            Assert.Contains("third", values);
        }

        #region Supporting Members

        private class ConcreteRecordBase : RecordBase
        {
            public ConcreteRecordBase(params string[] values)
                : base(false, values)
            {
            }

            public ConcreteRecordBase(bool readOnly, params string[] values)
                : base(readOnly, values)
            {
            }

            public ConcreteRecordBase(IList<string> values)
                : base(false, values)
            {
            }
        }

        #endregion
    }
}
