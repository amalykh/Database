   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5   	    DataBaseProject%   DataBaseProject.Views.LoginView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   U QCaliburn.Micro, Version=1.4.0.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  5   	   XiPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity  5   	     � Title$  Log in��5   	   $	��239��6   $	��319��6%   .��5      � $��	0,0,2,-21q�6   .+�5   
   �� $��Username��6   $
��Left=�6#   $��
10,100,0,0q�6>   $	��Top=�6R   �� 5	   
   $��Password��6   $
��Left=�6#   $��
13,138,0,0q�6>   $	��Top=�6R   �� 5
   
   -   5         ��Name$ Username��5
      $
��Left=�6$   $��23��6?   $��
87,100,0,0q�6K    �� TextWrapping$
 Wrap=�6_   $	��Top=�6s   $	��200��6�   G� 5   
   -   5        $��textPassword��5       G� Password$ 123456��6*   $
��Left=�6<   $��
87,141,0,0q�6W   $	��Top=�6k   $	��200��6�   $��23��6�   �� 5   
   $��Login��6   $
��Left=�6!   $	��Top=�6<   $��75��6T   $��212,169,0,0q�6_   .   (System.Windows.Interactivity.Interaction   Triggers 5      /   )System.Windows.Interactivity.EventTrigger  5         	EventName$ Click��6!   .   (System.Windows.Interactivity.TriggerBase   Actions. 5      "  Caliburn.Micro.ActionMessage     
MethodName$ Login��6(   5      5      5      �� 5   
   -   5        $��IP��5      $�� ��6   $
��Left=�6$   $��23��6?   $��	87,13,0,0q�6K   $
 Wrap=�6^   $	��Top=�6r   $	��200��6�   �� 5   
   -   5        $��
Tablespace��5      $��USERS��6$   $
��Left=�61   $��23��6L   $��	87,52,0,0q�6X   $
 Wrap=�6k   $	��Top=�6   $	��200��6�   �� 5   
   $��IP��6   $
��Left=�6   $��	13,10,0,0q�68   $	��Top=�6K   �� 5   
   $��
Tablespace��6   $
��Left=�6%   $��	13,49,0,0q�6@   $	��Top=�6S   5      5      