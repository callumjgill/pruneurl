import { ReactElement } from "react";
import { ToastContainer } from "react-bootstrap";
import {
  Notification,
  NotificationActions,
  NotificationState,
  useNotificationStore,
} from "../store/notifications";
import { NotificationToast } from "./NotificationToast";

export const NotificationToastContainer = (): ReactElement => {
  const [notifications, removeNotification] = useNotificationStore(
    (store: NotificationState & NotificationActions) => [
      store.notifications,
      store.removeNotification,
    ],
  );

  return (
    <ToastContainer className="p-3 position-fixed" position="bottom-center">
      {notifications.map((notification: Notification) => (
        <NotificationToast
          key={notification.id}
          message={notification.message}
          type={notification.type}
          onClose={() => removeNotification(notification.id)}
        />
      ))}
    </ToastContainer>
  );
};
