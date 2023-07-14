export const oneMinuteInMilliseconds = 60000;
export const oneSecondInMilliseconds = 1000;

export const waitAsync = (seconds: number): Promise<void> => {
  return new Promise((resolve) =>
    setTimeout(resolve, seconds * oneSecondInMilliseconds),
  );
};
