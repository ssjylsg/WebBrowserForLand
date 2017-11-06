namespace Cef3
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using Cef3.Interop;

    /// <summary>
    /// Class representing a list value. Can be used on any process and thread.
    /// </summary>
    public sealed unsafe partial class CefListValue
    {
        /// <summary>
        /// Creates a new object that is not owned by any other object.
        /// </summary>
        public static CefListValue Create()
        {
            return CefListValue.FromNative(
                cef_list_value_t.create()
                );
        }

        /// <summary>
        /// Returns true if this object is valid. Do not call any other methods if this
        /// method returns false.
        /// </summary>
        public bool IsValid
        {
            get { return cef_list_value_t.is_valid(_self) != 0; }
        }

        /// <summary>
        /// Returns true if this object is currently owned by another object.
        /// </summary>
        public bool IsOwned
        {
            get { return cef_list_value_t.is_owned(_self) != 0; }
        }

        /// <summary>
        /// Returns true if the values of this object are read-only. Some APIs may
        /// expose read-only objects.
        /// </summary>
        public bool IsReadOnly
        {
            get { return cef_list_value_t.is_read_only(_self) != 0; }
        }

        /// <summary>
        /// Returns a writable copy of this object.
        /// </summary>
        public CefListValue Copy()
        {
            return CefListValue.FromNative(
                cef_list_value_t.copy(_self)
                );
        }

        /// <summary>
        /// Sets the number of values. If the number of values is expanded all
        /// new value slots will default to type null. Returns true on success.
        /// </summary>
        public bool SetSize(int size)
        {
            return cef_list_value_t.set_size(_self, (UIntPtr)size) != 0;
        }

        /// <summary>
        /// Returns the number of values.
        /// </summary>
        public int Count
        {
            get { return (int)cef_list_value_t.get_size(_self); }
        }

        /// <summary>
        /// Removes all values. Returns true on success.
        /// </summary>
        public bool Clear()
        {
            return cef_list_value_t.clear(_self) != 0;
        }

        /// <summary>
        /// Removes the value at the specified index.
        /// </summary>
        public bool Remove(int index)
        {
            return cef_list_value_t.remove(_self, index) != 0;
        }

        /// <summary>
        /// Returns the value type at the specified index.
        /// </summary>
        public CefValueType GetValueType(int index)
        {
            return cef_list_value_t.get_type(_self, index);
        }

        /// <summary>
        /// Returns the value at the specified index as type bool.
        /// </summary>
        public bool GetBool(int index)
        {
            return cef_list_value_t.get_bool(_self, index) != 0;
        }

        /// <summary>
        /// Returns the value at the specified index as type int.
        /// </summary>
        public int GetInt(int index)
        {
            return cef_list_value_t.get_int(_self, index);
        }

        /// <summary>
        /// Returns the value at the specified index as type double.
        /// </summary>
        public double GetDouble(int index)
        {
            return cef_list_value_t.get_double(_self, index);
        }

        /// <summary>
        /// Returns the value at the specified index as type string.
        /// </summary>
        public string GetString(int index)
        {
            var n_result = cef_list_value_t.get_string(_self, index);
            return cef_string_userfree.ToString(n_result);
        }

        /// <summary>
        /// Returns the value at the specified index as type binary.
        /// </summary>
        public CefBinaryValue GetBinary(int index)
        {
            return CefBinaryValue.FromNativeOrNull(
                cef_list_value_t.get_binary(_self, index)
                );
        }

        /// <summary>
        /// Returns the value at the specified index as type dictionary.
        /// </summary>
        public CefDictionaryValue GetDictionary(int index)
        {
            return CefDictionaryValue.FromNativeOrNull(
                cef_list_value_t.get_dictionary(_self, index)
                );
        }

        /// <summary>
        /// Returns the value at the specified index as type list.
        /// </summary>
        public CefListValue GetList(int index)
        {
            return CefListValue.FromNativeOrNull(
                cef_list_value_t.get_list(_self, index)
                );
        }

        /// <summary>
        /// Sets the value at the specified index as type null. Returns true if the
        /// value was set successfully.
        /// </summary>
        public bool SetNull(int index)
        {
            return cef_list_value_t.set_null(_self, index) != 0;
        }

        /// <summary>
        /// Sets the value at the specified index as type bool. Returns true if the
        /// value was set successfully.
        /// </summary>
        public bool SetBool(int index, bool value)
        {
            return cef_list_value_t.set_bool(_self, index, value ? 1 : 0) != 0;
        }

        /// <summary>
        /// Sets the value at the specified index as type int. Returns true if the
        /// value was set successfully.
        /// </summary>
        public bool SetInt(int index, int value)
        {
            return cef_list_value_t.set_int(_self, index, value) != 0;
        }

        /// <summary>
        /// Sets the value at the specified index as type double. Returns true if the
        /// value was set successfully.
        /// </summary>
        public bool SetDouble(int index, double value)
        {
            return cef_list_value_t.set_double(_self, index, value) != 0;
        }

        /// <summary>
        /// Sets the value at the specified index as type string. Returns true if the
        /// value was set successfully.
        /// </summary>
        public bool SetString(int index, string value)
        {
            fixed (char* value_str = value)
            {
                var n_value = new cef_string_t(value_str, value != null ? value.Length : 0);
                return cef_list_value_t.set_string(_self, index, &n_value) != 0;
            }
        }

        /// <summary>
        /// Sets the value at the specified index as type binary. Returns true if the
        /// value was set successfully. After calling this method the |value| object
        /// will no longer be valid. If |value| is currently owned by another object
        /// then the value will be copied and the |value| reference will not change.
        /// Otherwise, ownership will be transferred to this object and the |value|
        /// reference will be invalidated.
        /// </summary>
        public bool SetBinary(int index, CefBinaryValue value)
        {
            return cef_list_value_t.set_binary(_self, index, value.ToNative()) != 0;
        }

        /// <summary>
        /// Sets the value at the specified index as type dict. Returns true if the
        /// value was set successfully. After calling this method the |value| object
        /// will no longer be valid. If |value| is currently owned by another object
        /// then the value will be copied and the |value| reference will not change.
        /// Otherwise, ownership will be transferred to this object and the |value|
        /// reference will be invalidated.
        /// </summary>
        public bool SetDictionary(int index, CefDictionaryValue value)
        {
            return cef_list_value_t.set_dictionary(_self, index, value.ToNative()) != 0;
        }

        /// <summary>
        /// Sets the value at the specified index as type list. Returns true if the
        /// value was set successfully. After calling this method the |value| object
        /// will no longer be valid. If |value| is currently owned by another object
        /// then the value will be copied and the |value| reference will not change.
        /// Otherwise, ownership will be transferred to this object and the |value|
        /// reference will be invalidated.
        /// </summary>
        public bool SetList(int index, CefListValue value)
        {
            return cef_list_value_t.set_list(_self, index, value.ToNative()) != 0;
        }
    }
}
