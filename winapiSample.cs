using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.InteropServices;
using AOT;
using Unity.Collections.LowLevel.Unsafe;

public class BitConvert
{
    [StructLayout(LayoutKind.Explicit)]
    struct EnumUnion32<T> where T : struct {
        [FieldOffset(0)]
        public T Enum;

        [FieldOffset(0)]
        public int Int;
    }

    public static int Enum32ToInt<T>(T e) where T : struct {
        var u = default(EnumUnion32<T>);
        u.Enum = e;
        return u.Int;
    }

    public static T IntToEnum32<T>(int value) where T : struct {
        var u = default(EnumUnion32<T>);
        u.Int = value;
        return u.Enum;
    }
}

public class Sample : MonoBehaviour
{
    class Resolution
    {
        int width;
        int height;

        Resolution(int w, int h)
        {
        }
    }

    List<Resolution> rr = new List<Resolution>() {

    };

    public enum EnumAA
    {
        One,
        TwoAbc = 675,
    }

    public InputField w;
    public InputField h;

    public Toggle fullS;

    public Text reps;

    public void OnClickREes()
    {
        reps.text = Screen.width + " x " + Screen.height;
    }


    public void OnClick()
    {
        
        Screen.SetResolution(int.Parse(w.text), int.Parse(h.text), fullS.isOn);
    }

    public void ShowMo()
    {
        Debug.Log("full " + Screen.fullScreenMode);
    }

     public void OnClick3()
    {
        SetDProc();
    }

    public void OnClick2()
    {
        //var rr  = add2(1,2);
        //Debug.Log("a " + rr);
        setMinWidth(512);
        setAspect(16f/9f, 16f/9f);

        SetMyWndProc();

        return;
        //var a = BitConvert.Enum32ToInt(EnumAA.TwoAbc);
        Debug.Log("a " + EnumSupport.ToInt(EnumAA.TwoAbc));
        Debug.Log("toname " + EnumSupport.GetName(EnumAA.TwoAbc));
        Debug.Log("fromname " + EnumSupport.FromName<EnumAA>("One"));
        return;
        int displayWidth = Screen.currentResolution.width;
        int displayHeight = Screen.currentResolution.height;

        Debug.Log("displayWidth " + displayWidth);
        Debug.Log("displayHeight " + displayHeight);

        var listw = new List<int>();
         var listh = new List<int>();


        foreach (var r in Screen.resolutions) {
            Debug.Log("# rw " + r.width);
            Debug.Log("rh " + r.height);
        }


        Debug.Log("end----");

        foreach (var r in Screen.resolutions) {
            if (r.width < r.height) {
                listw.Add(r.height);
                listh.Add(r.width);
            } else {
                listw.Add(r.width);
                listh.Add(r.height);

            }
        }

        for (int i =0; i< listh.Count; i++) {
            Debug.Log(listw[i] +" x " + listh[i]);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //SetDProc();

        var args = Environment.GetCommandLineArgs();
        foreach (var a in args) {
            Debug.Log("a " + a);
        }
        int displayWidth = Screen.currentResolution.width;
        int displayHeight = Screen.currentResolution.height;

        Debug.Log("displayWidth " + displayWidth);
        Debug.Log("displayHeight " + displayHeight);

        Debug.Log("width " + Screen.width);
        Debug.Log("hetigh " + Screen.height);

        //a();

        Debug.Log("width " + Screen.width);
        Debug.Log("hetigh " + Screen.height);

        lastWidth = Screen.width;
        lastHeight = Screen.height;
    }

    void a()
    {
        const float ratio = 16f / 9f;
        int displayWidth = Screen.currentResolution.width;
        int displayHeight = Screen.currentResolution.height;
        float displayAspctRatio = (float)displayWidth / displayHeight;
        float targetWidth = displayWidth;
        if (displayAspctRatio > ratio)
        {
            targetWidth = Mathf.FloorToInt(displayHeight * ratio);
        }
        float targetHeight = targetWidth / ratio;
        // ディスプレイの高さの0.8倍の大きさ、まだはディスプレイの高さー１００にする
            int windowedHeight = Mathf.FloorToInt(Mathf.Min(targetHeight * 0.8f, targetHeight - 100f));
            int windowedWidth = Mathf.FloorToInt(windowedHeight * ratio);
            Screen.SetResolution(windowedWidth, windowedHeight, false);
    }

    int lastWidth;
    int lastHeight;

    // Update is called once per frame
    void Update()
    {
        reps.text = Screen.width + " x " + Screen.height;
        return;
        if (Screen.width != lastWidth || Screen.height != lastHeight) {

            Debug.Log("--- frame ---");
            Debug.Log("width " + Screen.width);
            Debug.Log("hetigh " + Screen.height);

            const float ratio = 16f / 9f;
            int displayWidth = Screen.width;
            int displayHeight = Screen.height;
            float displayAspctRatio = (float)displayWidth / displayHeight;
            float targetWidth = displayWidth;
            if (displayAspctRatio > ratio)
            {
                targetWidth = Mathf.FloorToInt(displayHeight * ratio);
            }

            float targetHeight = targetWidth / ratio;
            int windowedHeight = Mathf.FloorToInt(targetHeight);
            int windowedWidth = Mathf.FloorToInt(windowedHeight * ratio);
            
            //Screen.SetResolution(windowedWidth, windowedHeight, false);
            //lastWidth = windowedWidth;
            //lastHeight = windowedHeight;
        }

        
    }


    [DllImport("WindowSizeProc", CallingConvention = CallingConvention.Cdecl)]
    private static extern int SetMyWndProc();

    [DllImport("WindowSizeProc", CallingConvention = CallingConvention.Cdecl)]
    private static extern void setMinWidth(int width);

     [DllImport("WindowSizeProc", CallingConvention = CallingConvention.Cdecl)]
    private static extern void setAspect(float min, float max);

    [DllImport("CloudDll1", CallingConvention = CallingConvention.Cdecl)]
    private static extern int add2(int a, int b);

    
    void SetDProc()
    {
         // ウインドウハンドルの取得
        hWnd = GetWindowHandle ();
 
        // ウインドウプロシージャをフックする
        //hMainWindow = GetForegroundWindow();
        newWndProc = new WndProcDelegate(wndProc);
        newWndProcPtr = Marshal.GetFunctionPointerForDelegate(newWndProc);
        //oldWndProcPtr = SetWindowLong(hMainWindow, -4, newWndProcPtr);
        oldWndProcPtr = SetWindowLongPtr(hWnd, -4, newWndProcPtr);
    
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    public static extern System.IntPtr GetForegroundWindow();
 
    [DllImport("user32.dll")]
    static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
    [DllImport("user32.dll")]
    static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
    [DllImport("user32.dll")]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);
    
    
    
    [DllImport("user32.dll")]
    static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
    
    [DllImport("user32.dll")]
    private static extern System.IntPtr GetActiveWindow();

    [DllImport("user32.dll", EntryPoint = "DefWindowProcA")]
    private static extern IntPtr DefWindowProc(IntPtr hWnd, uint wMsg, IntPtr wParam, IntPtr lParam);


    [DllImport("user32.dll")]
    private static extern bool GetClientRect(IntPtr hWnd, IntPtr rect);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, IntPtr rect);

    private static WndProcDelegate newWndProc;
    private static IntPtr oldWndProcPtr;
    private static IntPtr newWndProcPtr;
    private static IntPtr hWnd;

    private static float maxAspect = 16f / 9f;
    private static float minAspect = 16f / 9f;

    private static int minWidth = 512;

    private static int titleHeight = 70;

    public static System.IntPtr GetWindowHandle() {

        var hmd = GetActiveWindow();
        RectStruct cl = new RectStruct();
        IntPtr p1 = Marshal.AllocHGlobal(Marshal.SizeOf(cl));
        Marshal.StructureToPtr(cl, p1, false);
        GetClientRect(hmd, p1);


        RectStruct win = new RectStruct();;
        IntPtr p2 = Marshal.AllocHGlobal(Marshal.SizeOf(win));
        Marshal.StructureToPtr(win, p2, false);
        GetWindowRect(hmd, p2);

        unsafe {
                    RectStruct* prect = (RectStruct*)p1;
                    Debug.Log($"cl {prect->top} {prect->bottom} {prect->left} {prect->right}");
                    var h1 = prect->bottom - prect->top;
                   

                    prect = (RectStruct*)p2;
                    Debug.Log($"win {prect->top} {prect->bottom} {prect->left} {prect->right}");
                     var h2 = prect->bottom - prect->top;

                        RectStruct* prect2 = (RectStruct*)p2;
                    titleHeight = h2 - h1;
        }

        RectStruct cl2;
        cl2 = (RectStruct)Marshal.PtrToStructure(p1, typeof(RectStruct));
        Debug.Log($"{cl2.top} {cl2.bottom} {cl2.left} {cl2.right}");
        Debug.Log($"h {titleHeight}");
        Marshal.FreeHGlobal(p1);
        Marshal.FreeHGlobal(p2);


        return hmd;
    }

    [AOT.MonoPInvokeCallback(typeof(WndProcDelegate))]
    private static IntPtr wndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
    {
        /*
        width = prect->right - prect->left;
		height = prect->bottom - prect->top - _win_internal::titleHeight;
		aspect = (float)width / (float)height;
		aspect = min(aspect, _win_internal::maxAspect);
		aspect = max(aspect, _win_internal::minAspect);
        case WMSZ_TOP:
			width = max(height * aspect, _win_internal::minWidth);
			height = width / aspect;
			prect->right = prect->left + width;
			prect->top = prect->bottom - width / aspect - _win_internal::titleHeight;
			break;
		case WMSZ_BOTTOM:
			width = max(height * aspect, _win_internal::minWidth);
			height = width / aspect;
			prect->right = prect->left + width;
			prect->bottom = prect->top + width / aspect + _win_internal::titleHeight;
			break;
		case WMSZ_LEFT:
			width = max(width, _win_internal::minWidth);
			height = width / aspect;
			prect->left = prect->right - width;
			prect->bottom = prect->top + width / aspect + _win_internal::titleHeight;
			break;
		case WMSZ_RIGHT:
			width = max(width, _win_internal::minWidth);
			height = width / aspect;
			prect->right = prect->left + width;
			prect->bottom = prect->top + width / aspect + _win_internal::titleHeight;
			break;
		case WMSZ_TOPLEFT:
			width = max(_win_internal::minWidth, width);
			height = width / aspect;
			prect->top = prect->bottom - height - _win_internal::titleHeight;
			prect->left = prect->right - width;
			break;
		case WMSZ_TOPRIGHT:
			width = max(_win_internal::minWidth, width);
			height = width / aspect;
			prect->top = prect->bottom - height - _win_internal::titleHeight;
			prect->right = prect->left + width;
			break;
		case WMSZ_BOTTOMLEFT:
			width = max(_win_internal::minWidth, width);
			height = width / aspect;
			prect->bottom = prect->top + height + _win_internal::titleHeight;
			prect->left = prect->right - width;
			break;
		case WMSZ_BOTTOMRIGHT:
			width = max(_win_internal::minWidth, width);
			height = width / aspect;
			prect->bottom = prect->top + height + _win_internal::titleHeight;
			prect->right = prect->left + width;
			break;

            */
        var width = 0;
        var height = 0;
        var aspect = 0f;
        switch(msg) {
            case 0x0005:
                // WM_SIZE
                //Debug.Log("wndProc msg:" + msg + " wParam:" + wParam.ToInt32() + " lParam:" + lParam.ToInt32());
                // var param = lParam.ToInt32();
                // width = param & 0xFFFF;
                // height = (param >> 16) & 0xFFFF;
                // aspect = (float)width / (float)height;
                // if (aspect < minAspect) {
                //     SetWindowPos(hWnd, IntPtr.Zero, 0, 0, width, (int)(width / minAspect), 0x0002 | 0x0004);
                // }
                // Debug.Log($"w {width} h {height}");
            break;
            case 0x0214:
                // WM_SIZING
                //Debug.Log("wndProc msg:" + msg + " wParam:" + wParam.ToInt32() + " lParam:" + lParam.ToInt32());
                unsafe {
                    RectStruct* prect = (RectStruct*)lParam;
                    width = prect->right - prect->left;
                    height = prect->bottom - prect->top - titleHeight;
                    aspect = (float)width / (float)height;
                    aspect = Mathf.Min(aspect, maxAspect);
                    aspect = Mathf.Max(aspect, minAspect);


                    switch(wParam.ToInt32()) {
                        case 3: // TOP
                            width = (int)Mathf.Max(height * aspect, minWidth);
                            height = (int)(width / aspect);
                            prect->right = prect->left + width;
                            prect->top = prect->bottom - height - titleHeight;
                            break;
                        case 6:
                            width = (int)Mathf.Max(height * aspect, minWidth);
                            height = (int)(width / aspect);
                            prect->right = prect->left + width;
                            prect->bottom = prect->top + height + titleHeight;
                            break;
                        case 1: //WMSZ_LEFT:
                            width = (int)Mathf.Max(height *  aspect, minWidth);
                            prect->left = prect->right - width;
                            break;
                        case 2: //WMSZ_RIGHT:
                            width = Mathf.Max(width, minWidth);
                            var oh = height;
                             
                            height = (int)(width / aspect);

                            Debug.Log($"sizing {prect->top} {prect->bottom} {prect->left} {prect->right}");
                            Debug.Log($"w {width} as {aspect} h {height} ho {oh} th {titleHeight}");


                            prect->right = prect->left + width;
                            prect->bottom = prect->top + height + titleHeight;

                            //width = (int)Mathf.Max(height * aspect, minWidth);
                            //prect->right = prect->left + width;
                            break;
                        case 4 : //WMSZ_TOPLEFT:
                            width = Mathf.Max(minWidth, width);
                            height = (int)(width / aspect);
                            prect->top = prect->bottom - height;
                            prect->left = prect->right - width;
                            break;
                        case 5: //WMSZ_TOPRIGHT:
                            width = Mathf.Max(minWidth, width);
                            height = (int)(width / aspect);
                            prect->top = prect->bottom - height;
                            prect->right = prect->left + width;
                            break;
                        case 7: //WMSZ_BOTTOMLEFT:
                            width = Mathf.Max(minWidth, width);
                            height = (int)(width / aspect);
                            prect->bottom = prect->top + height;
                            prect->left = prect->right - width;
                            break;
                        case 8: //WMSZ_BOTTOMRIGHT:
                            width = Mathf.Max(minWidth, width);
                            height = (int)(width / aspect);
                            prect->bottom = prect->top + height;
                            prect->right = prect->left + width;
                            break;
                    }
                }

            break;
        }
        
 
        //return DefWindowProc(hWnd, msg, wParam, lParam);
        return CallWindowProc(oldWndProcPtr, hWnd, msg, wParam, lParam);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RectStruct
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    void OnDisable()
    {
        oldWndProcPtr = SetWindowLongPtr(hWnd, -4, oldWndProcPtr);
        hWnd = IntPtr.Zero;
        oldWndProcPtr = IntPtr.Zero;
        newWndProcPtr = IntPtr.Zero;
        newWndProc = null;
    }

    


}

