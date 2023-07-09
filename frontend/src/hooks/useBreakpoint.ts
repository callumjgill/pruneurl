import { useState, useEffect, useRef } from "react";
import {
  breakpoint,
  breakpointExtraLargeScreen,
  breakpointExtraSmallScreen,
  breakpointLargeScreen,
  breakpointMediumScreen,
  breakpointSmallScreen,
  extraLargeScreen,
  largeScreen,
  mediumScreen,
  smallScreen,
} from "../utils/breakpoints";

const calculateThreshold = 100;

const getDeviceConfig = (width: number): breakpoint => {
  if (width < smallScreen) {
    return breakpointExtraSmallScreen;
  }

  if (width >= smallScreen && width < mediumScreen) {
    return breakpointSmallScreen;
  }

  if (width >= mediumScreen && width < largeScreen) {
    return breakpointMediumScreen;
  }

  if (width >= largeScreen && width < extraLargeScreen) {
    return breakpointLargeScreen;
  }

  return breakpointExtraLargeScreen;
};

const useBreakpoint = () => {
  const [breakPoint, setBreakPoint] = useState(() =>
    getDeviceConfig(window.innerWidth),
  );

  const currentWidth = useRef(window.innerWidth);

  const calculateInnerWidth = () => {
    const newWidth = window.innerWidth;
    if (Math.abs(newWidth - currentWidth.current) > calculateThreshold) {
      setBreakPoint(getDeviceConfig(newWidth));
      currentWidth.current = newWidth;
    }
  };

  useEffect(() => {
    window.addEventListener("resize", calculateInnerWidth);
    return () => window.removeEventListener("resize", calculateInnerWidth);
  }, []);

  return breakPoint;
};

export default useBreakpoint;
