   M S B A M L   `   `   `  ����G  CBCSH2-Skrach, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:BCSH2_SkrachBCSH2_Skrach  5      +    %BCSH2_Skrach.View.AddSongToListWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ,localclr-namespace:BCSH2_Skrach     5        � Title$  Add Song to List��5      $	��450��6   $	��800��6(    � 	Resources 5      ,   &BCSH2_Skrach.ByteArrayToImageConverter   ByteArrayToImageConverter��%   &	          5   
   6*   5      .��5	      � ���#202123 � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   � .+�5      ]� $	��Top=�6   ���#202123.+�5      �� $��Skladby��6   $��30(�6+   
�������$��
70 20 5 20q�6P   $��Microsoft Sans Serif)�6d   5      5      �� 5   
   $��1��60   ���#202123��6   �� *Songs+ �� SelectionChanged 6R   �� *SelectedSongChangedCommand+��5      �� 5      .R�5      �  � ColumnDefinitions 5      �� 5      $
��Auto �6/   �� 5      $��* �6/   �� 5      $
��Auto �6/   �� 5      $
��Auto �6/   .+�5      �� $��0��6$   $��10,0,0,0q�6K   ��64   �� *Nazev+6K   �� 5       $��1��6$   $��10,0,0,0q�6W   ��64   �� *Interpreter.Jmeno+6W   �� 5!      $��2��6    $��50��6�   $��50��6�   $��10,0,0,0q�6�   ��60   �� *Album.Obrazek+ �� 	Converter# [  6�   5"      5#      5%      �� 5'   
    �� Orientation$ 
Horizontal=�6   $��2��6.   $��Center=�6;   ���#202123.+�5(      �� $��Vybrat��6   $��100px��6L   $��00 10q�6Z   ���	#FF141417
�������
�������$��1q�6�   ��6&   �� *SelectSongCommand+6�   5)      5*      5+      