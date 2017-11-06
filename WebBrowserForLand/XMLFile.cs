using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace WebBrowserForLand
{
    interface XMLFile
    {
        //��ȡXML�ĵ�����
        XmlDocument GetXML(string path);
        //����XML�ĵ�����
        void SaveXML(string path, XmlDocument xmldoc);
        //���ö��������д��XML��
        void ToXML(XmlDocument xmldoc);
        //�øö��������������XML�еĶ�Ӧ������ֵ
        void UpdateXML(XmlDocument xmldoc);
        /// <summary>
        /// ����XML�ļ��е�����ֵ
        /// </summary>
        /// <param name="path">XML�ļ�����</param>
        /// <param name="updateName">���µ�������</param>
        /// <param name="updateValue">���µ�ֵ</param>
        void SetAttribute(XmlDocument xmldoc, string updateName, string updateValue);
        //�����ж��ͬ���ڵ��XML�ļ�������ֵ
        void SetAttribute(XmlDocument xmldoc, int index, string updateName, string updateValue);
        /// <summary>
        /// ��XML�ļ��ж�ȡָ��������ֵ
        /// </summary>
        /// <param name="path">XML�ļ�</param>
        /// <param name="attributeName">��������</param>
        /// <returns>���ظ�����ֵ���ַ���</returns>
        string GetAttribute(XmlDocument xmldoc, string attributeName);
        //���ض��ͬ���ڵ������ֵ
        string GetAttribute(XmlDocument xmldoc, string attributeName, int index);
    }
}
