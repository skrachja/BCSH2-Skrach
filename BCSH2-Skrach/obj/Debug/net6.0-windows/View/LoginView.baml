   M S B A M L   `   `   `  ����G  CBCSH2-Skrach, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:BCSH2_SkrachBCSH2_Skrach  5          BCSH2_Skrach.LoginView   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:BCSH2_Skrach     5      $	��500��5   	   $	��300��5   	   $	��450��5      $	��280��5      $	��800��5      $	��400��5      .��5      � ���#202123  � ColumnDefinitions  5   
   �� 5      $
��241* �6   �� 5      $��9* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Center=�5      $��48(�5       
�������$��Login��5"      $��	0,10,0,80q�6   $��2��6-   � 5%   
   $��1��6   $��	0,20,0,60q�6   $	��400��5&       � IsSharedSizeScope$
 True��5'      $��2��6+   $��2��6?    5(      �� 5)      $
��Auto �6    �� 5*      $
��Auto �6    .+�5.      � $��0��6     5/      �� 50      $
��Auto �6'    z� SharedSizeGroup$ Label��64   �� 51      $��* �6'   .+�54      �� $��0��6   $��Username��6,   
�������6<   �� 55      $��1��6   $��10,0,0,0q�6*   ��56      �� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=�57      � 5:      $��1��6   $��0,10,0,0q�6      5;      �� 5<      $
��Auto �6'   $ Label��64   �� 5=      $��* �6'   .+�5@      �� $��0��6   $��Password��6,   
�������6<   &    BCSH2_Skrach.BindablePasswordBox  5A      $��1��6,   $��10,0,0,0q�6<      Password 5B      �� *
Password+$ PropertyChanged=� �� Mode$ TwoWay=�5C      5D      � 5H   
   $��2��6   $��0,20,0,0q�6     5I      �� 5J      $��* �6#   �� 5K      $��* �6#   .+�5N      �� $��0��6   $��0,0,10,0q�6%   $��Right=�5O      $��Login��5Q      $	��100��5R      $��40��6   
���   �
�������$��16(�5U      $
��Bold"�5V      ��5P      �� *LoginCommand+5V      �� 5X      $��1��6   $��10,0,0,0q�6%   $
��Left=�5Y      $��Register��5[      $	��100��5\      $��40��6   
���   �
�������$��16(�5_      $
��Bold"�5`      ��5Z      �� *RegisterCommand+5`      5a      5h      5i      