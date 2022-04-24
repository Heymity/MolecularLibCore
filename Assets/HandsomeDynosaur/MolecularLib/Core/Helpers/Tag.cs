/*  Copyright 2022 Gabriel Pasquale Rodrigues Scavone
*
*  Licensed under the Apache License, Version 2.0 (the "License");
*  you may not use this file except in compliance with the License.
*  You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
*  Unless required by applicable law or agreed to in writing, software
*  distributed under the License is distributed on an "AS IS" BASIS,
*  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
*  See the License for the specific language governing permissions and
*  limitations under the License.
*/

using UnityEngine;

namespace MolecularLib.Helpers
{
    [System.Serializable]
    public class Tag
    {
        [SerializeField] private string tag;

        public string TagName { get => tag; set => tag = value; }

        public bool CompareTag(GameObject goToCompareTag) => goToCompareTag.CompareTag(TagName);

        public static implicit operator string(Tag tag) => tag.TagName;
        public static implicit operator Tag(string tag) => new Tag(tag);

        public Tag(string tag)
        {
            TagName = tag;
        }

        public Tag() : this("Untagged") { }
    }

    public static class TagHelper
    {
        public static bool CompareTag(this GameObject go, Tag tagToBeEqual) => go.CompareTag(tagToBeEqual.TagName);
    }
}