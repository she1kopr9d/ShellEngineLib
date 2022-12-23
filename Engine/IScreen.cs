using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellEngineLib.Engine
{
    public interface IScreen
    {
        void Update();
        void ClearScreen();
        void Render();
        void VisibleMouse(bool visible);
        void SetPositionMouse(Math.Point position);
        void SetTitle(string title);
        void SetLimitFPS(uint fps);
    }
}
