using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public abstract class ControllerBase
    {
        protected readonly GameObject GameObject;
        protected readonly IServiceProvider ServiceProvider;

        protected ControllerBase(GameObject gameObject, IServiceProvider serviceProvider)
        {
            GameObject = gameObject;
            ServiceProvider = serviceProvider;
        }

        public virtual void Start() { }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }
    }
}
