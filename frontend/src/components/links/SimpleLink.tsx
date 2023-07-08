interface SimpleLinkProps {
  linkText: string;
  linkHref: string;
}

const SimpleLink = (props: SimpleLinkProps) => {
  const { linkText, linkHref } = { ...props };
  return (
    <a className="simple-link" href={linkHref} target="_blank" rel="noreferrer">
      {linkText}
    </a>
  );
};

export default SimpleLink;
