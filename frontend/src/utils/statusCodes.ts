export const isSuccess = (statusCode: number) => {
  return statusCode > 199 && statusCode < 300;
};

export const getDescriptionFromStatusCode = (statusCode: number) => {
  if (isSuccess(statusCode)) {
    return "Your URL has been pruned!";
  }

  switch (statusCode) {
    case 401:
      return "Unauthorized!";
    case 404:
      return "Cannot find resource!";
    case 500:
      return "An server error occured whilst submitting the url!";
    default:
      return "An unknown error occured whilst submitting the url!";
  }
};
