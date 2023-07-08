import { useCallback, useEffect, useState } from "react";
import { oneMinuteInMilliseconds } from "../../utils/time";

const SmallTimeAgo = () => {
  const [start] = useState<number>(Date.now);
  const [elapsedMinutes, setElapsedMinutes] = useState<number>(0);

  const updateTimeAgo = useCallback(() => {
    const elapsedMillisecondsSinceStart = Date.now() - start;
    const elapsedMinutesSinceStart = Math.floor(
      elapsedMillisecondsSinceStart / oneMinuteInMilliseconds,
    );
    setElapsedMinutes(elapsedMinutesSinceStart);
  }, [start]);

  useEffect(() => {
    const intervalId = setInterval(updateTimeAgo, oneMinuteInMilliseconds);
    return () => clearInterval(intervalId);
  }, [updateTimeAgo]);

  return <small>{elapsedMinutes} mins ago</small>;
};

export default SmallTimeAgo;
