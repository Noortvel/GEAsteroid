using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEAsteroid.GameFraemwork
{
    public abstract class AObject
    {
        private bool _isActiveSelf = true;
        private bool _isActiveInHier;
        private List<AObject> subObjects = new List<AObject>();
        public void AddSubObject(AObject obj)
        {
            obj._isActiveInHier = IsActive();
            obj.AbleHierh(IsActive());
            subObjects.Add(obj);
        }
        public void RemoveSubObject(AObject obj)
        {
            subObjects.Remove(obj);
        }
        public virtual void SetActive(bool active)
        {
            _isActiveSelf = active;
            _isActiveInHier = active;
            AbleHierh(active);
        }
        public virtual bool IsActive()
        {
            return _isActiveSelf && _isActiveInHier;
        }
        public virtual bool IsSelfActive()
        {
            return _isActiveSelf;
        }
        private void AbleHierh(bool active)
        {
            foreach (var x in subObjects)
            {
                x._isActiveInHier = active;
                x.AbleHierh(x.IsActive());
            }
        }
    }
}
