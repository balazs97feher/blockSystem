   M S B A M L   `   `   `  ����E  Asimulation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:simulation
simulation  5   	       simulation.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:simulation     5   	     � Title$  
Simulation��5   	   $
��1080��6   $
��1920��6*   .��5	      �� -   5         ��Name$ ApplicationCanvas��5	      .+�5
   
   �� $��Set direction��6   
 �� Top$	 492��6(    �� Bottom$ 20��69   $��20(�6L    �� Left$ 10��6Z   �� 5   
   -   5        $ 15��5      $	 529��6$   $	��129��65    �� SelectedIndex$ 0��6e   .@�5      �� .��6   	Clockwise6&   �� 5      .��6   Counterclockwise6-   5      �� 5   
   $��Set left switch��6   $	 492��6*   $ 20��6;   $��20(�6N   $	 168��6\   �� 5   
   -   5        $��Switch��5      $	 173��6"   $	 529��64   $��80��6E   $��22��6c   �� 5   
   $��Set right switch��6   $	 492��6+   $ 20��6<   $��20(�6O   $	 312��6]   �� 5   
   -   5        $��Switch��5      $	 317��6"   $	 529��64   $��80��6E   $��22��6P   6\   �� 5   
   $��Set departure��6   $	 492��6(   $ 20��69   $��20(�6L   $	 476��6Z   �� 5   
   -   5        $	 481��5      $	 529��6%   $	��129��66   $��22��6B   $ 0��6r   .@�5      �� $��Track#1��6   �� 5      $��Track#2��6   5      �� 5   
   $��	Set speed��6   $	 566��6$   $ 20��65   $��20(�6H   $ 10��6V   �� 5   
   -   5        $��
SpeedSlide��5      $ 15��6>   $	 608��6O   $	��280��6`    � Maximum$	 120Q�6l    �� TickPlacement$ BottomRight=�6z    �� TickFrequency$ 10Q�6�   	 �� IsSnapToTickEnabled	 . 
 � Value
 6#   �� *
SetSpeed+6�   �� 5   
   $��24��6   $	 300��6    �� TextWrapping$
 Wrap=�60   $	 608��6�   $��45��6�   $��20(�6�   ��6D   ��  �� ElementName$ 
SpeedSlide�� �� Path$ Value� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   $��Current speed:��6   $	 671��6)   $ 20��6:   $��20(�6M   $ 10��6[   $	��144��6l   �� 5    
   -   5         ��Name$ DisplayVelocity��5       $	 154��6-   $
��Wrap=�6?   $	 646��6j   $��62��6{   $	��133��6�   $��48(�6�   ��6S   �� *Speed+6�   5"      5#      