import { MutableRefObject, useCallback, useRef, useState } from "react";

interface UseProgressReturn {
  now: number;
  decreaseProgress: () => void;
}

const useProgress = (): UseProgressReturn => {
  const internalNow: MutableRefObject<number> = useRef<number>(100);
  const [now, setNow] = useState<number>(internalNow.current);
  const decreaseProgress = useCallback(() => {
    internalNow.current -= 1;
    setNow(internalNow.current);
  }, []);
  return { now, decreaseProgress };
};

export default useProgress;
