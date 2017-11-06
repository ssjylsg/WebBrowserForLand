//
// DO NOT MODIFY! THIS IS AUTOGENERATED FILE!
//
namespace Cef3.Interop
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Security;
    
    [StructLayout(LayoutKind.Sequential, Pack = libcef.ALIGN)]
    [SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable")]
    internal unsafe struct cef_domdocument_t
    {
        internal cef_base_t _base;
        internal IntPtr _get_type;
        internal IntPtr _get_document;
        internal IntPtr _get_body;
        internal IntPtr _get_head;
        internal IntPtr _get_title;
        internal IntPtr _get_element_by_id;
        internal IntPtr _get_focused_node;
        internal IntPtr _has_selection;
        internal IntPtr _get_selection_start_node;
        internal IntPtr _get_selection_start_offset;
        internal IntPtr _get_selection_end_node;
        internal IntPtr _get_selection_end_offset;
        internal IntPtr _get_selection_as_markup;
        internal IntPtr _get_selection_as_text;
        internal IntPtr _get_base_url;
        internal IntPtr _get_complete_url;
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int add_ref_delegate(cef_domdocument_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int release_delegate(cef_domdocument_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int get_refct_delegate(cef_domdocument_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate CefDomDocumentType get_type_delegate(cef_domdocument_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_domnode_t* get_document_delegate(cef_domdocument_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_domnode_t* get_body_delegate(cef_domdocument_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_domnode_t* get_head_delegate(cef_domdocument_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_string_userfree* get_title_delegate(cef_domdocument_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_domnode_t* get_element_by_id_delegate(cef_domdocument_t* self, cef_string_t* id);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_domnode_t* get_focused_node_delegate(cef_domdocument_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int has_selection_delegate(cef_domdocument_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_domnode_t* get_selection_start_node_delegate(cef_domdocument_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int get_selection_start_offset_delegate(cef_domdocument_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_domnode_t* get_selection_end_node_delegate(cef_domdocument_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate int get_selection_end_offset_delegate(cef_domdocument_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_string_userfree* get_selection_as_markup_delegate(cef_domdocument_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_string_userfree* get_selection_as_text_delegate(cef_domdocument_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_string_userfree* get_base_url_delegate(cef_domdocument_t* self);
        
        [UnmanagedFunctionPointer(libcef.CEF_CALLBACK)]
        #if !DEBUG
        [SuppressUnmanagedCodeSecurity]
        #endif
        private delegate cef_string_userfree* get_complete_url_delegate(cef_domdocument_t* self, cef_string_t* partialURL);
        
        // AddRef
        private static IntPtr _p0;
        private static add_ref_delegate _d0;
        
        public static int add_ref(cef_domdocument_t* self)
        {
            add_ref_delegate d;
            var p = self->_base._add_ref;
            if (p == _p0) { d = _d0; }
            else
            {
                d = (add_ref_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(add_ref_delegate));
                if (_p0 == IntPtr.Zero) { _d0 = d; _p0 = p; }
            }
            return d(self);
        }
        
        // Release
        private static IntPtr _p1;
        private static release_delegate _d1;
        
        public static int release(cef_domdocument_t* self)
        {
            release_delegate d;
            var p = self->_base._release;
            if (p == _p1) { d = _d1; }
            else
            {
                d = (release_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(release_delegate));
                if (_p1 == IntPtr.Zero) { _d1 = d; _p1 = p; }
            }
            return d(self);
        }
        
        // GetRefCt
        private static IntPtr _p2;
        private static get_refct_delegate _d2;
        
        public static int get_refct(cef_domdocument_t* self)
        {
            get_refct_delegate d;
            var p = self->_base._get_refct;
            if (p == _p2) { d = _d2; }
            else
            {
                d = (get_refct_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_refct_delegate));
                if (_p2 == IntPtr.Zero) { _d2 = d; _p2 = p; }
            }
            return d(self);
        }
        
        // GetType
        private static IntPtr _p3;
        private static get_type_delegate _d3;
        
        public static CefDomDocumentType get_type(cef_domdocument_t* self)
        {
            get_type_delegate d;
            var p = self->_get_type;
            if (p == _p3) { d = _d3; }
            else
            {
                d = (get_type_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_type_delegate));
                if (_p3 == IntPtr.Zero) { _d3 = d; _p3 = p; }
            }
            return d(self);
        }
        
        // GetDocument
        private static IntPtr _p4;
        private static get_document_delegate _d4;
        
        public static cef_domnode_t* get_document(cef_domdocument_t* self)
        {
            get_document_delegate d;
            var p = self->_get_document;
            if (p == _p4) { d = _d4; }
            else
            {
                d = (get_document_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_document_delegate));
                if (_p4 == IntPtr.Zero) { _d4 = d; _p4 = p; }
            }
            return d(self);
        }
        
        // GetBody
        private static IntPtr _p5;
        private static get_body_delegate _d5;
        
        public static cef_domnode_t* get_body(cef_domdocument_t* self)
        {
            get_body_delegate d;
            var p = self->_get_body;
            if (p == _p5) { d = _d5; }
            else
            {
                d = (get_body_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_body_delegate));
                if (_p5 == IntPtr.Zero) { _d5 = d; _p5 = p; }
            }
            return d(self);
        }
        
        // GetHead
        private static IntPtr _p6;
        private static get_head_delegate _d6;
        
        public static cef_domnode_t* get_head(cef_domdocument_t* self)
        {
            get_head_delegate d;
            var p = self->_get_head;
            if (p == _p6) { d = _d6; }
            else
            {
                d = (get_head_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_head_delegate));
                if (_p6 == IntPtr.Zero) { _d6 = d; _p6 = p; }
            }
            return d(self);
        }
        
        // GetTitle
        private static IntPtr _p7;
        private static get_title_delegate _d7;
        
        public static cef_string_userfree* get_title(cef_domdocument_t* self)
        {
            get_title_delegate d;
            var p = self->_get_title;
            if (p == _p7) { d = _d7; }
            else
            {
                d = (get_title_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_title_delegate));
                if (_p7 == IntPtr.Zero) { _d7 = d; _p7 = p; }
            }
            return d(self);
        }
        
        // GetElementById
        private static IntPtr _p8;
        private static get_element_by_id_delegate _d8;
        
        public static cef_domnode_t* get_element_by_id(cef_domdocument_t* self, cef_string_t* id)
        {
            get_element_by_id_delegate d;
            var p = self->_get_element_by_id;
            if (p == _p8) { d = _d8; }
            else
            {
                d = (get_element_by_id_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_element_by_id_delegate));
                if (_p8 == IntPtr.Zero) { _d8 = d; _p8 = p; }
            }
            return d(self, id);
        }
        
        // GetFocusedNode
        private static IntPtr _p9;
        private static get_focused_node_delegate _d9;
        
        public static cef_domnode_t* get_focused_node(cef_domdocument_t* self)
        {
            get_focused_node_delegate d;
            var p = self->_get_focused_node;
            if (p == _p9) { d = _d9; }
            else
            {
                d = (get_focused_node_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_focused_node_delegate));
                if (_p9 == IntPtr.Zero) { _d9 = d; _p9 = p; }
            }
            return d(self);
        }
        
        // HasSelection
        private static IntPtr _pa;
        private static has_selection_delegate _da;
        
        public static int has_selection(cef_domdocument_t* self)
        {
            has_selection_delegate d;
            var p = self->_has_selection;
            if (p == _pa) { d = _da; }
            else
            {
                d = (has_selection_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(has_selection_delegate));
                if (_pa == IntPtr.Zero) { _da = d; _pa = p; }
            }
            return d(self);
        }
        
        // GetSelectionStartNode
        private static IntPtr _pb;
        private static get_selection_start_node_delegate _db;
        
        public static cef_domnode_t* get_selection_start_node(cef_domdocument_t* self)
        {
            get_selection_start_node_delegate d;
            var p = self->_get_selection_start_node;
            if (p == _pb) { d = _db; }
            else
            {
                d = (get_selection_start_node_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_selection_start_node_delegate));
                if (_pb == IntPtr.Zero) { _db = d; _pb = p; }
            }
            return d(self);
        }
        
        // GetSelectionStartOffset
        private static IntPtr _pc;
        private static get_selection_start_offset_delegate _dc;
        
        public static int get_selection_start_offset(cef_domdocument_t* self)
        {
            get_selection_start_offset_delegate d;
            var p = self->_get_selection_start_offset;
            if (p == _pc) { d = _dc; }
            else
            {
                d = (get_selection_start_offset_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_selection_start_offset_delegate));
                if (_pc == IntPtr.Zero) { _dc = d; _pc = p; }
            }
            return d(self);
        }
        
        // GetSelectionEndNode
        private static IntPtr _pd;
        private static get_selection_end_node_delegate _dd;
        
        public static cef_domnode_t* get_selection_end_node(cef_domdocument_t* self)
        {
            get_selection_end_node_delegate d;
            var p = self->_get_selection_end_node;
            if (p == _pd) { d = _dd; }
            else
            {
                d = (get_selection_end_node_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_selection_end_node_delegate));
                if (_pd == IntPtr.Zero) { _dd = d; _pd = p; }
            }
            return d(self);
        }
        
        // GetSelectionEndOffset
        private static IntPtr _pe;
        private static get_selection_end_offset_delegate _de;
        
        public static int get_selection_end_offset(cef_domdocument_t* self)
        {
            get_selection_end_offset_delegate d;
            var p = self->_get_selection_end_offset;
            if (p == _pe) { d = _de; }
            else
            {
                d = (get_selection_end_offset_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_selection_end_offset_delegate));
                if (_pe == IntPtr.Zero) { _de = d; _pe = p; }
            }
            return d(self);
        }
        
        // GetSelectionAsMarkup
        private static IntPtr _pf;
        private static get_selection_as_markup_delegate _df;
        
        public static cef_string_userfree* get_selection_as_markup(cef_domdocument_t* self)
        {
            get_selection_as_markup_delegate d;
            var p = self->_get_selection_as_markup;
            if (p == _pf) { d = _df; }
            else
            {
                d = (get_selection_as_markup_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_selection_as_markup_delegate));
                if (_pf == IntPtr.Zero) { _df = d; _pf = p; }
            }
            return d(self);
        }
        
        // GetSelectionAsText
        private static IntPtr _p10;
        private static get_selection_as_text_delegate _d10;
        
        public static cef_string_userfree* get_selection_as_text(cef_domdocument_t* self)
        {
            get_selection_as_text_delegate d;
            var p = self->_get_selection_as_text;
            if (p == _p10) { d = _d10; }
            else
            {
                d = (get_selection_as_text_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_selection_as_text_delegate));
                if (_p10 == IntPtr.Zero) { _d10 = d; _p10 = p; }
            }
            return d(self);
        }
        
        // GetBaseURL
        private static IntPtr _p11;
        private static get_base_url_delegate _d11;
        
        public static cef_string_userfree* get_base_url(cef_domdocument_t* self)
        {
            get_base_url_delegate d;
            var p = self->_get_base_url;
            if (p == _p11) { d = _d11; }
            else
            {
                d = (get_base_url_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_base_url_delegate));
                if (_p11 == IntPtr.Zero) { _d11 = d; _p11 = p; }
            }
            return d(self);
        }
        
        // GetCompleteURL
        private static IntPtr _p12;
        private static get_complete_url_delegate _d12;
        
        public static cef_string_userfree* get_complete_url(cef_domdocument_t* self, cef_string_t* partialURL)
        {
            get_complete_url_delegate d;
            var p = self->_get_complete_url;
            if (p == _p12) { d = _d12; }
            else
            {
                d = (get_complete_url_delegate)Marshal.GetDelegateForFunctionPointer(p, typeof(get_complete_url_delegate));
                if (_p12 == IntPtr.Zero) { _d12 = d; _p12 = p; }
            }
            return d(self, partialURL);
        }
        
    }
}
