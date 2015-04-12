﻿/***************************************************************************
 *   Skills.cs
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
using System.Collections.Generic;

namespace UltimaXNA.Ultima.Player
{
    public class SkillData
    {
        Dictionary<int, SkillEntry> m_skills = new Dictionary<int,SkillEntry>();
        bool m_skillsLoaded = false;

        public Dictionary<int, SkillEntry> List
        {
            get
            {
                if (!m_skillsLoaded)
                {
                    m_skillsLoaded = true;
                    foreach (IO.Skill skill in IO.SkillsData.List)
                        m_skills.Add(skill.ID, new SkillEntry(skill.ID, skill.Index, skill.UseButton, skill.Name, 0.0f, 0.0f, 0, 0.0f));
                }
                return m_skills;
            }
        }

        public SkillEntry SkillEntry(int skillID)
        {
            if (List.Count > skillID)
                return List[skillID];
            else
                return null;
        }
    }

    public class SkillEntry
    {
        private int m_id;
        private int m_index;
        private bool m_hasUseButton;
        private string m_name;
        private float m_value;
        private float m_valueUnmodified;
        private byte m_lockType;
        private float m_cap;

        public int ID
        {
            get { return m_id; }
            set { m_id = value; }
        }
        public int Index
        {
            get { return m_index; }
            set { m_index = value; }
        }
        public bool HasUseButton
        {
            get { return m_hasUseButton; }
            set { m_hasUseButton = value; }
        }
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }
        public float Value
        {
            get { return m_value; }
            set { m_value = value; }
        }
        public float ValueUnmodified
        {
            get { return m_valueUnmodified; }
            set { m_valueUnmodified = value; }
        }
        public byte LockType
        {
            get { return m_lockType; }
            set { m_lockType = value; }
        }
        public float Cap
        {
            get { return m_cap; }
            set { m_cap = value; }
        }

        public SkillEntry(int id, int index, bool useButton, string name, float value, float unmodified, byte locktype, float cap)
        {
            ID = id;
            Index = index;
            HasUseButton = useButton;
            Name = name;
            Value = value;
            ValueUnmodified = unmodified;
            LockType = locktype;
            Cap = cap;
        }
    }
}
