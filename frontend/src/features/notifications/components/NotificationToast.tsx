import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { ReactElement, useEffect, useState } from "react";
import { ProgressBar, Toast } from "react-bootstrap";
import useProgress from "../hooks/useProgress";
import useNotificationToastOptions from "../hooks/useNotificationToastOptions";
import { NotificationType } from "../store/notifications";

interface NotificationToastProps {
  message: string;
  type: NotificationType;
  onClose: () => void;
}

const notificationDelay: number = 10000; // ms
const callbackDelay: number = Math.floor(notificationDelay / 100);

export const NotificationToast = ({
  message,
  type,
  onClose,
}: NotificationToastProps): ReactElement => {
  const [show, setShow] = useState<boolean>(true);
  const { now, decreaseProgress } = useProgress();
  const { variant, title, icon } = useNotificationToastOptions({ type });

  useEffect(() => {
    if (!show || now > 0) {
      return;
    }

    setShow(false);
  }, [show, now]);

  useEffect(() => {
    if (!show || now <= 0) {
      return;
    }

    const timer = setTimeout(() => {
      decreaseProgress();
    }, callbackDelay);

    return () => clearTimeout(timer);
  });

  return (
    <Toast
      bg={variant}
      show={show}
      onClose={() => setShow(false)}
      onExited={onClose}
    >
      <Toast.Header>
        <FontAwesomeIcon className="me-2" icon={icon} />
        <strong className="me-auto">{title}</strong>
      </Toast.Header>
      <div className="bg-white">
        <Toast.Body>{message}</Toast.Body>
        <ProgressBar className="rounded-0" now={now} variant={variant} />
      </div>
    </Toast>
  );
};
