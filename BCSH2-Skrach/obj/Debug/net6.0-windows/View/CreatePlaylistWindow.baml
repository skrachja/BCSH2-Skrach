   M S B A M L   `   `   `  ����G  CBCSH2-Skrach, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:BCSH2_Skrach.ViewBCSH2_Skrach.View  5   	   ,    &BCSH2_Skrach.View.CreatePlaylistWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:BCSH2_Skrach.View     5   	     � Title$  Create Playlist��5   	   $	��200��5	   	   $	��400��5
   	   $	��200��5   	   $	��400��5   	   .��5      � ���#202123.+�5   
   �� .+�5      � $��10q�6    � ColumnDefinitions 5      �� 5      $��150px �6'   �� 5      $��* �6'   .+�5      �� $��Playlist Name:��6   $��0��61   
�������$��12(�6X   $
��Bold"�6f   �� 5      $��1��68   ���	#FF141417
�������
�������$��1q�6�   ��6   �� *PlaylistName+6�   5      � 5      $��10q�6    5      �� 5      $��150px �6'   �� 5      $��* �6'   .+�5      �� $��Image:��6   $��0��6)   
�������$��12(�6P   $
��Bold"�6^   �� 5       $��Select Image��6   $��1��6W   ���	#FF141417
�������
�������$��1q�6�   ��60   �� *SelectImageCommand+6�   5!      �� 5$       �� Orientation$ 
Horizontal=�6   $��Center=�62   .+�5%      �� $��OK��6   $��100px��6&   $��00 10q�64   ���	#FF141417
�������
�������$��1q�6�   ��6C   �� *CreatePlaylistCommand+��6m   ��  �� RelativeSource �� *Self+6�   �� 5&      $��Cancel��6   $��100px��6*   $��10 10q�68   ���	#FF141417
�������
�������$��1q�6�   ��6G   �� *CancelCommand+��6i   ��  �� *Self+6�   5'      5(      5)      5*      